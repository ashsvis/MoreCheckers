using System;
using System.Collections.Specialized;
using System.ServiceModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.ServiceModel.Channels;
using Checkers.CheckersServiceReference;

namespace Checkers
{
    public static partial class Client
    {
        public static void UpdateOpponentGameAsync(Guid gameId)
        {
            Task.Run(() => 
            {
                if (_proxy != null &&
                    (_proxy.State == CommunicationState.Opened || _proxy.State == CommunicationState.Created))
                _proxy.UpdateGame(_clientId, gameId);
            });
        }

        public static Task<DateTime> GetDateAsync()
        {
            var task = new Task<DateTime>(GetDate);
            task.Start();
            return task;
        }

        public static DateTime GetDate()
        {
            return GetMethod("GetDate", () => _proxy.GetDate(), DateTime.MinValue);
        }

        public static T GetMethod<T>(string methodName, Func<T> func, T def)
        {
            var n = Repeat;
            while (true)
            {
                try
                {
                    return _proxy != null &&
                        (_proxy.State == CommunicationState.Opened ||
                        _proxy.State == CommunicationState.Created)
                        ? func()
                        : def;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(500);
                    n--;
                    if (n >= 0) continue;
                    var mess =
                        string.Format("Ошибка вызова метода {0}() : {1}", methodName, ex.Message);
                    _showError?.Invoke(mess);
                    return def;
                }
            }
        }

        public static T[] GetArrayMethod<T>(string methodName, Func<T[]> func, T[] def)
        {
            var n = Repeat;
            while (true)
            {
                try
                {
                    return _proxy != null &&
                        (_proxy.State == CommunicationState.Opened ||
                        _proxy.State == CommunicationState.Created)
                        ? func()
                        : def;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(500);
                    n--;
                    if (n >= 0) continue;
                    var mess =
                        string.Format("Ошибка вызова метода {0}() : {1}", methodName, ex.Message);
                    _showError?.Invoke(mess);
                    return def;
                }
            }
        }

        public static NameValueCollection Convert(string[] props)
        {
            var nvc = new NameValueCollection();
            foreach (var vals in props.Select(para => para.Split(new[] { '\t' }))
                .Where(vals => vals.Length == 2))
            {
                nvc[vals[0]] = vals[1];
            }
            return nvc;
        }

        #region Служебные процедуры

        private static readonly Guid _clientId = Guid.NewGuid();
        private static ClientErrorWrapper _showError;
        private static System.Timers.Timer _faultTimer;
        private static CheckersServiceClient _proxy;
        private static string _host;
        private static int _port;
        private const int Repeat = 10;

        private static ConnectionUpdate _connectionUpdate;
        private static GameUpdateWrapper _update;

        public static void Connect(string host, int port,
            ClientErrorWrapper showError, ConnectionUpdate connectionUpdate, GameUpdateWrapper update)
        {
            _host = host;
            _port = port;
            _showError = showError;
            _connectionUpdate = connectionUpdate;
            _update = update;
            new Thread(() =>
            {
                _proxy = new DataExchange(host, port, ConnectionUpdated, update).GetProxy();
            }).Start();
            _faultTimer = new System.Timers.Timer(5 * 1000) { AutoReset = false };
            _faultTimer.Elapsed += Reconnecting;
        }

        public static bool RegisterForUpdates(Player player)
        {
            return GetMethod("RegisterForUpdates", () => _proxy.RegisterForUpdates(_clientId, player), false);
        }

        public static Task<bool> RegisterForUpdatesAsync(Player player)
        {
            var task = new Task<bool>(() => RegisterForUpdates(player));
            task.Start();
            return task;
        }

        private static void ConnectionUpdated(ConnectionState state)
        {
            switch (state)
            {
                case ConnectionState.Opened:
                case ConnectionState.Opening:
                case ConnectionState.Closing:
                case ConnectionState.Closed:
                    _connectionUpdate(state);
                    break;
                case ConnectionState.Faulted:
                    _connectionUpdate(ConnectionState.Faulted);
                    _faultTimer.Enabled = true;
                    break;
            }
        }

        private static void Reconnecting(object sender, ElapsedEventArgs e)
        {
            const string mess = "Попытка подключиться к шашечному серверу после сбоя связи...";
            _showError?.Invoke(mess);
            Reconnect();
        }

        public static void Disconnect()
        {
            
        }

        public static void Reconnect(string host = null, int port = 0)
        {
            if (host != null)
            {
                _host = host;
                _port = port;
            }
            new Thread(() =>
            {
                Disconnect();
                Thread.Sleep(500);
                Connect(_host, _port, _showError, _connectionUpdate, _update);
            }).Start();
        }

        #endregion
    }

    public delegate void GameUpdateWrapper(GameStatus gameStatus);


    public delegate void ClientErrorWrapper(string errormessage);

    public enum ConnectionState
    {
        Opening,
        Opened,
        Closing,
        Closed,
        Faulted
    }

    public delegate void ConnectionUpdate(ConnectionState state);

    public class DataExchange : ICheckersServiceCallback
    {
        private readonly TimeSpan _timeout = new TimeSpan(0, 1, 30);
        private readonly InstanceContext _site;
        private readonly Binding _binding;
        private readonly CheckersServiceClient _proxy;
        private readonly ConnectionUpdate _connectionUpdate;
        private readonly GameUpdateWrapper _update;

        public DataExchange(string host, int port, ConnectionUpdate connectionUpdate, GameUpdateWrapper update)
        {
            _update = update;
            _connectionUpdate = connectionUpdate;
            var uri = (host == null || host.Trim().Length == 0 ||
                host.ToLower().Equals("localhost") ||
                       host.Equals("127.0.0.1"))
                          ? String.Format("net.tcp://localhost:{0}/CheckersAppServer", port)
                          : String.Format("net.tcp://{0}:{1}/CheckersAppServer", host.Trim(), port);
            _site = new InstanceContext(this);
            _binding = new NetTcpBinding
            {
                OpenTimeout = _timeout,
                SendTimeout = _timeout,
                ReceiveTimeout = _timeout,
                CloseTimeout = _timeout
            };
            _proxy = new CheckersServiceClient(_site, _binding, new EndpointAddress(uri));
            _proxy.InnerDuplexChannel.Opened += (sender, args) =>
            {
                _connectionUpdate?.Invoke(ConnectionState.Opened);
            };
            _proxy.InnerDuplexChannel.Opening += (sender, args) =>
            {
                _connectionUpdate?.Invoke(ConnectionState.Opening);
            };
            _proxy.InnerDuplexChannel.Closed += (sender, args) =>
            {
                _connectionUpdate?.Invoke(ConnectionState.Closed);
            };
            _proxy.InnerDuplexChannel.Faulted += (sender, args) =>
            {
                _connectionUpdate?.Invoke(ConnectionState.Faulted);
            };
        }

        public void GameUpdated(GameStatus gameStatus)
        {
            _update?.Invoke(gameStatus);
        }

        public CheckersServiceClient GetProxy()
        {
            return _proxy;
        }
    }
}

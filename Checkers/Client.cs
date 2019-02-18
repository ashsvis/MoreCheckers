using System;
using System.Collections.Specialized;
using System.ServiceModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.ServiceModel.Channels;

namespace Checkers
{
    public static partial class Client
    {

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

        private static ClientErrorWrapper _showError;
        private static System.Timers.Timer _faultTimer;
        private static CheckersServiceReference.CheckersServiceClient _proxy;
        private static string _host;
        private static int _port;
        private const int Repeat = 10;

        private static ConnectionUpdate _connectionUpdate;

        public static void Connect(string host, int port,
            ClientErrorWrapper showError, ConnectionUpdate connectionUpdate)
        {
            _host = host;
            _port = port;
            _showError = showError;
            _connectionUpdate = connectionUpdate;
            new Thread(() =>
            {
                _proxy = (new DataExchange(host, port, ConnectionUpdated)).GetProxy();
                Thread.Sleep(100);
            }).Start();
            _faultTimer = new System.Timers.Timer(5 * 1000) { AutoReset = false };
            _faultTimer.Elapsed += Reconnecting;
        }

        private static void ConnectionUpdated(ConnectionState state)
        {
            switch (state)
            {
                case ConnectionState.Opening:
                case ConnectionState.Opened:
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
            //if (_call != null) new Thread(() => _call.Disconnect()).Start();
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
                Connect(_host, _port, _showError, _connectionUpdate);
            }).Start();
        }

        #endregion
    }

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

    public class DataExchange
    {
        private readonly TimeSpan _timeout = new TimeSpan(0, 1, 30);
        private readonly Binding _binding;
        private readonly CheckersServiceReference.CheckersServiceClient _proxy;
        private readonly ConnectionUpdate _connectionUpdate;

        public DataExchange(string host, int port, ConnectionUpdate connectionUpdate)
        {
            _connectionUpdate = connectionUpdate;
            var uri = (host == null || host.Trim().Length == 0 ||
                host.ToLower().Equals("localhost") ||
                       host.Equals("127.0.0.1"))
                          ? String.Format("net.tcp://localhost:{0}/CheckersAppServer", port)
                          : String.Format("net.tcp://{0}:{1}/CheckersAppServer", host.Trim(), port);
            _binding = new NetTcpBinding
            {
                OpenTimeout = _timeout,
                SendTimeout = _timeout,
                ReceiveTimeout = _timeout,
                CloseTimeout = _timeout
            };
            _proxy = new CheckersServiceReference.CheckersServiceClient(_binding, new EndpointAddress(uri));
            _proxy.InnerChannel.Opening += (sender, args) =>
            {
                _connectionUpdate?.Invoke(ConnectionState.Opening);
            };
            _proxy.InnerChannel.Opened += (sender, args) =>
            {
                _connectionUpdate?.Invoke(ConnectionState.Opened);
            };
            _proxy.InnerChannel.Faulted += (sender, args) =>
            {
                _connectionUpdate?.Invoke(ConnectionState.Faulted);
            };
            _proxy.InnerChannel.Closing += (sender, args) =>
            {
                _connectionUpdate?.Invoke(ConnectionState.Closing);
            };
            _proxy.InnerChannel.Closed += (sender, args) =>
            {
                _connectionUpdate?.Invoke(ConnectionState.Closed);
            };
        }

        public CheckersServiceReference.CheckersServiceClient GetProxy()
        {
            return _proxy;
        }
    }
}

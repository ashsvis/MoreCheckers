﻿using Checkers.CheckersServiceReference;
using Checkers.Properties;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    public partial class CheckersForm : Form
    {
        private string _host = "localhost";
        private int _dataPort = 5528;
        private bool _connected;
        private bool _started;
        private Point _offsetBoard;
        private Engine _engineBoard;
        private Player _player = Player.White;

        public CheckersForm()
        {
            InitializeComponent();

            DoubleBuffered = true;


            _host = Settings.Default.ServerHost;
            _dataPort = Settings.Default.ServerPort;

            _offsetBoard = new Point(0, mainMenu.Height);
            _engineBoard = new Engine();
        }

        private void CheckersForm_Load(object sender, EventArgs e)
        {
            tsslIpAddress.Text = _host;

            CenterToScreen();

            // соединяемся к сервису без блокировки интерфейса
            new Task(() =>
            {
                // подключение к серверу
                Connect();
            }).Start();

        }

        private void CheckersForm_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            // рисуем доску с шашками
            if (_player == Player.White)
                graphics.TranslateTransform(_offsetBoard.X, _offsetBoard.Y);
            else
            {
                // "переворачиваем" доску
                var c = new Point((int)(_boardRect.X + _boardRect.Width / 2 - _offsetBoard.X * 1.5),
                                  (int)(_boardRect.Y + _boardRect.Height / 2 - _offsetBoard.Y * 1.5));
                graphics.TranslateTransform(c.X, c.Y);
                graphics.RotateTransform(180);
                graphics.TranslateTransform(-c.X, -c.Y);
            }
            lock (_engineBoard)
            {
                _engineBoard.Draw(graphics);
            }
        }

        /// <summary>
        /// Метод открытия соединения с сервером приложения
        /// </summary>
        private void Connect()
        {
            Client.Connect(_host, _dataPort, ShowStatus, ConnectionUpdated, UpdateBoard);
            var n = 10;
            while (true)
            {
                var dt = Client.GetDate();
                if (dt != DateTime.MinValue)
                    break;
                Thread.Sleep(500);
                n--;
                if (n < 0) break;
            }
        }

        /// <summary>
        /// Метод вызывается системой уведомления сервера, 
        /// инициируемой вызовом на клиенте Client.UpdateOpponentGame(_gameGuid)
        /// и запрашивает свежий скрипт у сервера, чтобы распарсить его и перерисовать содержимое доски
        /// </summary>
        /// <param name="gameStatus"></param>
        private async void UpdateBoard(GameStatus gameStatus)
        {
            var script = await Client.GetDrawBoardScriptAsync(_gameGuid, _player);
            lock (_engineBoard)
            {
                _engineBoard.Parse(script);
            }
            Invalidate();
            UpdateStatusText(gameStatus);
        }

        private void ConnectionUpdated(ConnectionState state)
        {
            switch (state)
            {
                case ConnectionState.Opening:
                    ShowStatus("Открытие соединения...");
                    break;
                case ConnectionState.Opened:
                    _connected = true;
                    ShowStatus("Выберите новую игру...");
                    CreateNewGame();
                    break;
                case ConnectionState.Closing:
                    _connected = false;
                    ShowStatus("Закрытие соединения...");
                    break;
                case ConnectionState.Closed:
                    _connected = false;
                    ShowStatus("Соединение закрыто");
                    break;
                case ConnectionState.Faulted:
                    _connected = false;
                    ShowStatus("Соединение не установлено");
                    break;
            }
        }

        private void ShowStatus(string errormessage)
        {
            var method = new MethodInvoker(() => 
            {
                lbStatus.Text = errormessage;
                mainStatus.Refresh();
            });
            if (InvokeRequired)
                BeginInvoke(method);
            else
                method();
        }

        private async void UpdateGameStatus()
        {
            var status = await Client.GetGameStatusAsync(_gameGuid);
            UpdateStatusText(status);
        }

        private void UpdateStatusText(GameStatus status)
        {
            if (!status.Exists || string.IsNullOrWhiteSpace(status.Text)) return;
            var method = new MethodInvoker(() =>
            {
                lbStatus.Text = status.Text;
                mainStatus.Refresh();
                lbWhiteScore.Text = $"Белые: {status.WhiteScore}";
                lbBlackScore.Text = $"Чёрные: {status.BlackScore}";
                if (lvLog.Items.Count != status.Log.Length ||
                    lvLog.Items.Count > 0 && lvLog.Items.Count == status.Log.Length &&
                    lvLog.Items[lvLog.Items.Count - 1].SubItems[2].Text != (status.Log[status.Log.Length - 1].Black ?? ""))
                {
                    lvLog.Items.Clear();
                    foreach (var item in status.Log)
                    {
                        var lvi = new ListViewItem(item.Number.ToString());
                        lvi.SubItems.Add(item.White);
                        lvi.SubItems.Add(item.Black);
                        lvLog.Items.Add(lvi);
                        lvi.EnsureVisible();
                    }
                }
            });
            if (InvokeRequired)
                BeginInvoke(method);
            else
                method();
        }

        /// <summary>
        /// Метод проверки соединения с сервером приложения
        /// </summary>
        private async void UpdateClock()
        {
            var date = await Client.GetDateAsync();
            tsslDateTime.Text = date != DateTime.MinValue
                ? date.ToString("dd.MM.yyyy, ddd, HH.mm.ss") : "Нет связи с сервером";
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            // получаем текущую дату и время с сервера
            // заодно (и это важно) периодический вызов диагностирует состояние связи
            UpdateClock();
        }

        private Point Mirror(Point point)
        {
            return _player == Player.White 
                ? point 
                : new Point(_boardRect.Width - point.X, _boardRect.Height - point.Y);
        }


        private void CheckersForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                OnBoardMouseDown(Mirror(e.Location), (int)ModifierKeys, _player);

        }

        private async void OnBoardMouseDown(Point location, int modifierKeys, Player player)
        {
            var p = new Point(location.X - _offsetBoard.X, location.Y - _offsetBoard.Y);
            if (await Client.OnBoardMouseDownAsync(_gameGuid, p, modifierKeys, player))
                Client.UpdateOpponentGameAsync(_gameGuid);
        }

        private Point _lastLocation;

        private void CheckersForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_lastLocation != e.Location)
            {
                _lastLocation = e.Location;
                OnBoardMouseMove(Mirror(e.Location), (int)ModifierKeys, _player);
            }
        }

        private async void OnBoardMouseMove(Point location, int modifierKeys, Player player)
        {
            var p = new Point(location.X - _offsetBoard.X, location.Y - _offsetBoard.Y);
            if (await Client.OnBoardMouseMoveAsync(_gameGuid, p, modifierKeys, player))
                Client.UpdateOpponentGameAsync(_gameGuid);
        }

        private void CheckersForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                OnBoardMouseUp(Mirror(e.Location), (int)ModifierKeys, _player);
        }

        private async void OnBoardMouseUp(Point location, int modifierKeys, Player player)
        {
            var p = new Point(location.X - _offsetBoard.X, location.Y - _offsetBoard.Y);
            if (await Client.OnBoardMouseUpAsync(_gameGuid, p, modifierKeys, player))
                Client.UpdateOpponentGameAsync(_gameGuid);
        }

        private void tsmiGame_DropDownOpening(object sender, EventArgs e)
        {
            tsmiNewGame.Enabled = _connected;
            tsmiEndGame.Enabled = _connected && _started;
        }

        private void tsmiNewGame_Click(object sender, EventArgs e)
        {
            if (_started)
            {
                if (DialogForm.Show(this, "Завершить текущую игру?", "Шашки") != DialogResult.Yes) return;
                EndGame(_gameGuid);
                _started = false;
                Client.UpdateOpponentGameAsync(_gameGuid);
                Client.DestroyGame(_gameGuid);
                _gameGuid = Client.CreateGame();
            }
            var frm = new ChooseGameForm(_gameGuid)
            {
                PlayerName = Settings.Default.PlayerName
            };
            var result = frm.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                Settings.Default.PlayerName = frm.PlayerName;
                Settings.Default.Save();
                if (frm.PlayMode == PlayMode.NetGame && _gameGuid != frm.OpponentGameGuid)
                {
                    _gameGuid = frm.OpponentGameGuid;
                    _player = Player.Black;
                    JoinNewGame(frm.PlayerName);
                }
                else
                {
                    _player = Player.White;
                    StartNewGame(frm.PlayMode, _player, frm.PlayerName);
                   
                }
                Client.UpdateOpponentGameAsync(_gameGuid);
                _started = true;
            }
        }

        private async void StartNewGame(PlayMode playMode, Player player, string playerName)
        {
            await Client.StartNewGameAsync(_gameGuid, playMode, player, playerName);
        }

        private async void JoinNewGame(string playerName)
        {
            await Client.JoinNewGameAsync(_gameGuid, playerName);
        }

        private async void EndGame(Guid guid)
        {
            await Client.EndGameAsync(guid);
        }

        private Guid _gameGuid;

        private Rectangle _boardRect;

        private async void CreateNewGame()
        {
            await Client.RegisterForUpdatesAsync(_player);
            _started = false;
            _gameGuid = await Client.CreateGameAsync();
            var script = await Client.GetDrawBoardScriptAsync(_gameGuid, _player);
            lock (_engineBoard)
            {
                _engineBoard.Parse(script);
            }
            var method = new MethodInvoker(() =>
            {
                lock (_engineBoard)
                {
                    if (_engineBoard.Rects.ContainsKey("BoardRect"))
                    {
                        var size = Size.Ceiling(_engineBoard.Rects["BoardRect"].Size);
                        if (tsmiShowGamePanel.Checked)
                            size.Width += panelGame.Width;
                        size.Height += mainMenu.Height + mainStatus.Height;
                        ClientSize = size;
                        _boardRect = new Rectangle(_offsetBoard, size);
                    }
                }
                Invalidate();
            });
            if (InvokeRequired)
                BeginInvoke(method);
            else
                method();
        }

        private void tsmiServerIpAddress_Click(object sender, EventArgs e)
        {
            var frm = new ServerIpAddressForm() { IpAddress = _host };
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _host = frm.IpAddress;
                tsslIpAddress.Text = _host;
                Settings.Default.ServerHost = _host;
                Settings.Default.Save();
                Client.Reconnect(_host, _dataPort);
            }
        }

        private void tsmiShowGamePanel_Click(object sender, EventArgs e)
        {
            panelGame.Visible = tsmiShowGamePanel.Checked;
            var size = ClientSize;
            if (tsmiShowGamePanel.Checked)
                size.Width += panelGame.Width;
            else
                size.Width -= panelGame.Width;
            ClientSize = size;
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsmiEndGame_Click(object sender, EventArgs e)
        {
            if (_started && DialogForm.Show(this, "Завершить текущую игру?", "Шашки") != DialogResult.Yes) return;
            Client.UpdateOpponentGameAsync(_gameGuid);
            EndGame();
            Client.DestroyGame(_gameGuid);
            _gameGuid = Client.CreateGame();
            _started = false;
        }

        private async void EndGame()
        {
            if (await Client.EndGameAsync(_gameGuid))
            {
                _started = false;
                var script = await Client.GetDrawBoardScriptAsync(_gameGuid, _player);
                lock (_engineBoard)
                {
                    _engineBoard.Parse(script);
                }
                Invalidate();
            }
        }

        private void CheckersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_started)
            {
                if (DialogForm.Show(this, "Завершить текущую игру?", "Шашки") != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }
            EndGame(_gameGuid);
            Client.UpdateOpponentGameAsync(_gameGuid);
            Client.DestroyGame(_gameGuid);
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            DialogForm.Show(this, "Русские шашки." + Environment.NewLine + 
                "Программирование на заказ: ashsvis@gmail.com", "Шашки", false);
        }

        private void tsmiRules_Click(object sender, EventArgs e)
        {
            new RulesForm().ShowDialog(this);
        }
    }

}

using Checkers.CheckersServiceReference;
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
            graphics.TranslateTransform(_offsetBoard.X, _offsetBoard.Y);
            _engineBoard.Draw(graphics);
        }

        /// <summary>
        /// Метод открытия соединения с сервером приложения
        /// </summary>
        private void Connect()
        {
            Client.Connect(_host, _dataPort, ShowStatus, ConnectionUpdated);
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

        private void ConnectionUpdated(ConnectionState state)
        {
            switch (state)
            {
                case ConnectionState.Opening:
                    ShowStatus("Открытие соединения...");
                    break;
                case ConnectionState.Opened:
                    _connected = true;
                    ShowStatus("Соединение установлено");
                    CreateNewGame();
                    CenterBoardToScreen();
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

        private void CenterBoardToScreen()
        {
            var method = new MethodInvoker(() =>
            {
                CenterToScreen();
            });
            if (InvokeRequired)
                BeginInvoke(method);
            else
                method();
        }

        private void ShowStatus(string errormessage)
        {
            var method = new MethodInvoker(() => 
            {
                status.Text = errormessage;
                mainStatus.Refresh();
            });
            if (InvokeRequired)
                BeginInvoke(method);
            else
                method();
        }

        //private bool _board_AskQuestion(string text, string caption)
        //{
        //    return MessageBox.Show(this, text, caption, MessageBoxButtons.YesNo,
        //        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        //}

        //private void _board_ActivePlayerChanged()
        //{
        //    //_engineBoard.Parse(_io.DrawBoardScript());
        //    Invalidate();
        //    UpdateStatus();
        //    //_net.SendNetGameStatus();
        //}

        private async void UpdateStatusText()
        {
            var text = await Client.GetGameStatusAsync(_gameGuid);
            var method = new MethodInvoker(() =>
            {
                status.Text = text;
                mainStatus.Refresh();
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
            UpdateStatusText();
        }

        private void CheckersForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OnBoardMouseDown(e.Location, (int)ModifierKeys, _player);

                //var n = lvLog.SelectedIndices.Count > 0 ? lvLog.SelectedIndices[0] : -1;
                //if (n < 0 || n == _game.Log.Count - 1)
                //{
                //    _io.MouseDown(e.Location);
                //    _engineBoard.Parse(_io.DrawBoardScript());
                //    Invalidate();
                //}
                //else
                //    if (_board_AskQuestion("Продолжить игру?", "Шашки"))
                //{
                //    var item = lvLog.Items[_game.Log.Count - 1];
                //    item.Selected = true;
                //}
            }

        }

        private async void OnBoardMouseDown(Point location, int modifierKeys, Player player)
        {
            var p = new Point(location.X - _offsetBoard.X, location.Y - _offsetBoard.Y);
            if (await Client.OnBoardMouseDownAsync(_gameGuid, p, modifierKeys, player))
            {
                _engineBoard.Parse(await Client.GetDrawBoardScriptAsync(_gameGuid, player));
                Invalidate();
            }
        }

        private void CheckersForm_MouseMove(object sender, MouseEventArgs e)
        {
            OnBoardMouseMove(e.Location, (int)ModifierKeys, _player);
        }

        private async void OnBoardMouseMove(Point location, int modifierKeys, Player player)
        {
            var p = new Point(location.X - _offsetBoard.X, location.Y - _offsetBoard.Y);
            if (await Client.OnBoardMouseMoveAsync(_gameGuid, p, modifierKeys, player))
            {
                _engineBoard.Parse(await Client.GetDrawBoardScriptAsync(_gameGuid, player));
                Invalidate();
            }
        }

        private void CheckersForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OnBoardMouseUp(e.Location, (int)ModifierKeys, _player);
            }
        }

        private async void OnBoardMouseUp(Point location, int modifierKeys, Player player)
        {
            var p = new Point(location.X - _offsetBoard.X, location.Y - _offsetBoard.Y);
            if (await Client.OnBoardMouseUpAsync(_gameGuid, p, modifierKeys, player))
            {
                _engineBoard.Parse(await Client.GetDrawBoardScriptAsync(_gameGuid, player));
                Invalidate();
            }
        }

        private void lvLog_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //e.Item = new ListViewItem();
            //var item = _game.Log[e.ItemIndex];
            //e.Item.Text = item.Number.ToString();
            //e.Item.SubItems.Add(item.White);
            //e.Item.SubItems.Add(item.Black);
        }

        private void lvLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvLog.SelectedIndices.Count == 0) return;
            var n = lvLog.SelectedIndices[0];
            //_board.Selected = null;

            //var semiSteps = _game.Log[n].GetMapSemiSteps();
            //foreach (var item in semiSteps)
            //{
            //    var map = item.DeepClone();
            //    _board.SetMap(map);
            //    Refresh();
            //    Thread.Sleep(200);
            //}

            //if (n == _game.Log.Count - 1)
            //{
            //    lvLog.SelectedIndices.Clear();
            //    UpdateStatus();
            //}
            //else
            //    UpdateStatusText(string.Format("Положение фигур после {0}-го хода.", n + 1));
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
                if (MessageBox.Show(this, "Завершить текущую игру?", "Шашки",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            }
            var frm = new ChooseGameForm(_gameGuid);
            var result = frm.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                if (frm.PlayMode == PlayMode.NetGame && _gameGuid != frm.OpponentGameGuid)
                {
                    _gameGuid = frm.OpponentGameGuid;
                    _player = Player.Black;
                }
                else
                    _player = Player.White;
                StartNewGame(frm.PlayMode);
                _started = true;
                Invalidate();
            }
        }

        private async void StartNewGame(PlayMode playMode)
        {
            await Client.StartNewGameAsync(_gameGuid, playMode);
        }

        private Guid _gameGuid;

        private async void CreateNewGame()
        {
            _started = false;
            _gameGuid = await Client.CreateGameAsync();
            _engineBoard.Parse(await Client.GetDrawBoardScriptAsync(_gameGuid, _player));
            var method = new MethodInvoker(() =>
            {
                if (_engineBoard.Rects.ContainsKey("BoardRect"))
                {
                    var size = Size.Ceiling(_engineBoard.Rects["BoardRect"].Size);
                    if (tsmiShowGamePanel.Checked)
                        size.Width += panelGame.Width;
                    size.Height += mainMenu.Height + mainStatus.Height;
                    ClientSize = size;
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
            if (_started)
            {
                if (MessageBox.Show(this, "Завершить текущую игру?", "Шашки",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            }
            EndGame();
        }

        private async void EndGame()
        {
            if (await Client.EndGameAsync(_gameGuid))
            {
                _started = false;
                _engineBoard.Parse(await Client.GetDrawBoardScriptAsync(_gameGuid, _player));
                Invalidate();
            }
        }

        private void CheckersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_started)
            {
                if (MessageBox.Show(this, "Завершить текущую игру?", "Шашки",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }
            Client.DestroyGame(_gameGuid);
        }
    }

}

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
        private Io _io;
        private Board _board;
        private Game _game;

        private string _host = "localhost";
        private int _dataPort = 5528;
        private bool _connected;
        private Engine _engineBoard;
        private Engine _engineChecker;

        public CheckersForm()
        {
            InitializeComponent();

            DoubleBuffered = true;
            _game = new Game() { Mode = PlayMode.SelfGame };
            _board = new Board(_game);
            _board.UpdateStatus += () => UpdateStatus();
            _board.ShowError += _board_ShowError;
            _board.AskQuestion += _board_AskQuestion;
            _board.ActivePlayerChanged += _board_ActivePlayerChanged;
            _board.CheckerMoved += _board_CheckerMoved;
            //_net = new NetGame(_game, _board);
            //_net.DisplayPeerMessage += _net_DisplayPeerMessage;
            //_net.CaptionChanged += _net_CaptionChanged;
            _io = new Io(_game, _board, new Size(0, mainMenu.Height + mainTools.Height));

            _engineBoard = new Engine();
            _engineChecker = new Engine();

            var txt = @"
m1 = 35;23
m2 = 35;56
m3 = 67;56
m4 = 74;49
m5 = 74;23
m6 = 74;34

TEXT  = Speed
TEXT2 = P34

offset 50 75
scale m1 2,5
rotate m1 45

//frame
poly m1 m2 m3 m4 m5
FillOpacity = 100
FillColor   = 0;0;251
DrawColor   = 255;0;0
draw
fill

//text1
FillOpacity = 150
FillColor   = 0;251;251
FontSize    = 9
text m1 TEXT
fill

//text2
FillOpacity = 250
FillColor   = 149;255;255
FontSize    = 15
TextAlign   = BottomLeft
text m6 TEXT2
fill";
        }

        private void CheckersForm_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            // рисуем доску с шашками
            //_io.DrawBoard(graphics);
            _engineBoard.Draw(graphics);
            _engineChecker.Draw(graphics);
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
                    //SaveConnectConfig();
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
                tsslDateTime.Text = errormessage;
                mainStatus.Refresh();
            });
            if (InvokeRequired)
                BeginInvoke(method);
            else
                method();
        }

        private void _board_CheckerMoved(bool direction, Address startPos, Address endPos, MoveResult moveResult, int stepCount)
        {
            UpdateLog(direction, startPos, endPos, moveResult, stepCount);
            tsmiSaveGame.Enabled = tsbSaveGame.Enabled = true;
        }

        private void UpdateLog(bool direction, Address startPos, Address endPos, MoveResult moveResult, int stepCount)
        {
            var result = string.Format("{0}{1}{2}",
                    startPos, moveResult == MoveResult.SuccessfullCombat ? ":" : "-", endPos);
            if (!direction)
            {
                // ходят "белые"
                if (stepCount == 1) // первый ход (из, возможно, серии ходов)
                {
                    var item = new LogItem() { Number = _game.Log.Count + 1, White = result };
                    item.AddToMap(_board.GetMap().DeepClone());
                    _game.Log.Add(item);
                    lvLog.VirtualListSize = _game.Log.Count;
                    var lvi = lvLog.Items[lvLog.Items.Count - 1];
                    lvLog.FocusedItem = lvi;
                    lvi.EnsureVisible();
                    lvLog.Invalidate();
                }
                else
                {
                    var item = _game.Log[_game.Log.Count - 1];
                    item.White += ":" + endPos;
                    item.AddToMap(_board.GetMap().DeepClone());
                    lvLog.Invalidate();
                }
            }
            else
            {
                // ходят "чёрные"
                var item = _game.Log[_game.Log.Count - 1];
                if (stepCount == 1) // первый ход (из, возможно, серии ходов)
                    item.Black = result;
                else
                    item.Black += ":" + endPos;
                item.AddToMap(_board.GetMap().DeepClone());
                lvLog.Invalidate();
            }
        }

        private void _board_ShowError(string text, string caption)
        {
            //var status = string.Format(_game.Direction
            //                            ? "Ход чёрных ({0})..." 
            //                            : "Ход белых ({0})...", text.ToLower().TrimEnd('!'));
            UpdateStatusText(_game.ToString());
            //MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool _board_AskQuestion(string text, string caption)
        {
            return MessageBox.Show(this, text, caption, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        private void _board_ActivePlayerChanged()
        {
            _engineBoard.Parse(_io.DrawBoardScript());
            Invalidate();
            UpdateStatus();
            //_net.SendNetGameStatus();
        }

        private void UpdateStatus()
        {
            lbWhiteScore.Text = string.Format("Белые: {0}", _game.WhiteScore);
            lbBlackScore.Text = string.Format("Чёрные: {0}", _game.BlackScore);
            //UpdateCaptionText(_net.Caption);
            UpdateStatusText(_game.ToString());
        }

        private void UpdateStatusText(string text)
        {
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

        private void UpdateCaptionText(string text)
        {
            var method = new MethodInvoker(() =>
            {
                Text = text;
            });
            if (InvokeRequired)
                BeginInvoke(method);
            else
                method();
        }

        private void CheckersForm_Load(object sender, EventArgs e)
        {
            // Конфигурирование игры
            _board.ResetMap();
            var size = _io.GetDrawBoardSize();
            size.Width += panelLog.Width;
            size.Height += mainMenu.Height + mainTools.Height + mainStatus.Height;
            ClientSize = size;
            //UpdateStatus();
            CenterToScreen();

            // соединяемся к сервису без блокировки интерфейса
            new Task(() =>
            {
                // подключение к серверу
                Connect();
            }).Start();

            _engineBoard.Parse(_io.DrawBoardScript());
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

        private void CheckersForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var n = lvLog.SelectedIndices.Count > 0 ? lvLog.SelectedIndices[0] : -1;
                if (n < 0 || n == _game.Log.Count - 1)
                {
                    _io.MouseDown(e.Location);
                    _engineBoard.Parse(_io.DrawBoardScript());
                    Invalidate();
                }
                else
                    if (_board_AskQuestion("Продолжить игру?", "Шашки"))
                {
                    var item = lvLog.Items[_game.Log.Count - 1];
                    item.Selected = true;
                }
            }

        }

        private void CheckersForm_MouseMove(object sender, MouseEventArgs e)
        {
            _io.MouseMove(e.Location);
            Invalidate();
        }

        private void CheckersForm_MouseUp(object sender, MouseEventArgs e)
        {
            _io.MouseUp(e.Location);
            _engineBoard.Parse(_io.DrawBoardScript());
            Invalidate();
        }

        private void tsmiTools_DropDownOpening(object sender, EventArgs e)
        {
            tsmiSelfGame.Checked = _game.WinPlayer == WinPlayer.Game && _game.Mode == PlayMode.SelfGame;
            tsmiAutoGame.Checked = _game.WinPlayer == WinPlayer.Game && _game.Mode == PlayMode.Game;
            tsmiNetGame.Checked = _game.WinPlayer == WinPlayer.Game && _game.Mode == PlayMode.NetGame;
            tsmiSelfGame.Enabled = tsmiAutoGame.Enabled = tsmiNetGame.Enabled = _game.WinPlayer != WinPlayer.Game;
        }

        private void tsmiSelfGame_Click(object sender, EventArgs e)
        {
            var last = _game.Mode;
            _game.Mode = PlayMode.SelfGame;
            //if (last == PlayMode.NetGame)
                //NetStop();
            _game.WinPlayer = WinPlayer.Game;
            UpdateStatus();
        }

        private void lvLog_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem();
            var item = _game.Log[e.ItemIndex];
            e.Item.Text = item.Number.ToString();
            e.Item.SubItems.Add(item.White);
            e.Item.SubItems.Add(item.Black);
        }

        private void lvLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvLog.SelectedIndices.Count == 0) return;
            var n = lvLog.SelectedIndices[0];
            _board.Selected = null;

            var semiSteps = _game.Log[n].GetMapSemiSteps();
            foreach (var item in semiSteps)
            {
                var map = item.DeepClone();
                _board.SetMap(map);
                Refresh();
                Thread.Sleep(200);
            }

            if (n == _game.Log.Count - 1)
            {
                lvLog.SelectedIndices.Clear();
                UpdateStatus();
            }
            else
                UpdateStatusText(string.Format("Положение фигур после {0}-го хода.", n + 1));
        }
    }

}

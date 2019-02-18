using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Checkers
{
    public partial class CheckersForm : Form
    {
        private Io _io;
        private Board _board;
        private Game _game;

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

        private void CheckersForm_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            // рисуем доску с шашками
            _io.DrawBoard(graphics);
        }
    }

}

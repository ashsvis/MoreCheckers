﻿using System;
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
        private Point _offsetBoard;
        private Engine _engineBoard;

        public CheckersForm()
        {
            InitializeComponent();

            DoubleBuffered = true;

            _offsetBoard = new Point(0, mainMenu.Height + mainTools.Height);
            _engineBoard = new Engine();
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

        //private void UpdateStatusText(string text)
        //{
        //    var method = new MethodInvoker(() =>
        //    {
        //        status.Text = text;
        //        mainStatus.Refresh();
        //    });
        //    if (InvokeRequired)
        //        BeginInvoke(method);
        //    else
        //        method();
        //}

        private void CheckersForm_Load(object sender, EventArgs e)
        {
            CenterToScreen();

            // соединяемся к сервису без блокировки интерфейса
            new Task(() =>
            {
                // подключение к серверу
                Connect();
            }).Start();

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
                OnBoardMouseDown(e.Location, (int)ModifierKeys);

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

        private async void OnBoardMouseDown(Point location, int modifierKeys)
        {
            var p = new Point(location.X - _offsetBoard.X, location.Y - _offsetBoard.Y);
            if (await Client.OnBoardMouseDownAsync(_gameGuid, p, modifierKeys))
            {
                _engineBoard.Parse(await Client.GetDrawBoardScriptAsync(_gameGuid));
                Invalidate();
            }
        }

        private void CheckersForm_MouseMove(object sender, MouseEventArgs e)
        {
            OnBoardMouseMove(e.Location, (int)ModifierKeys);
        }

        private async void OnBoardMouseMove(Point location, int modifierKeys)
        {
            var p = new Point(location.X - _offsetBoard.X, location.Y - _offsetBoard.Y);
            if (await Client.OnBoardMouseMoveAsync(_gameGuid, p, modifierKeys))
            {
                _engineBoard.Parse(await Client.GetDrawBoardScriptAsync(_gameGuid));
                Invalidate();
            }
        }

        private void CheckersForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OnBoardMouseUp(e.Location, (int)ModifierKeys);
            }
        }

        private async void OnBoardMouseUp(Point location, int modifierKeys)
        {
            var p = new Point(location.X - _offsetBoard.X, location.Y - _offsetBoard.Y);
            if (await Client.OnBoardMouseUpAsync(_gameGuid, p, modifierKeys))
            {
                _engineBoard.Parse(await Client.GetDrawBoardScriptAsync(_gameGuid));
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
            tsbNewGame.Enabled = tsmiOpenGame.Enabled = _connected;
        }

        private void tsmiNewGame_Click(object sender, EventArgs e)
        {
            CreateNewGame();
        }

        private Guid _gameGuid;

        private async void CreateNewGame()
        {
            _gameGuid = await Client.CreateGameAsync();
            _engineBoard.Parse(await Client.GetDrawBoardScriptAsync(_gameGuid));
            if (_engineBoard.Rects.ContainsKey("BoardRect"))
            {
                var size = Size.Ceiling(_engineBoard.Rects["BoardRect"].Size);
                size.Width += panelLog.Width;
                size.Height += mainMenu.Height + mainTools.Height + mainStatus.Height;
                ClientSize = size;
            }
            CenterToScreen();
            Invalidate();
        }
    }

}

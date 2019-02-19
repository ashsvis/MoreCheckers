using System;
using System.Collections.Generic;
using System.Drawing;

namespace Checkers
{
    public class Io
    {
        const int CellSize = 50;    // размер стороны клетки в пикселах
        const int BorderWidth = 20; // ширина бордюра в пикселах
        Board _board;
        Game _game;
        Size _topLeftSize;
        List<Cell> _hoverCells;

        public Io(Game game, Board board, Size topLeftSize)
        {
            _game = game;
            _board = board;
            _topLeftSize = topLeftSize;
            _hoverCells = new List<Cell>();
        }

        public void SetGame(Game game)
        {
            _game = game;
        }

        /// <summary>
        /// Получение расчётного размера доски
        /// </summary>
        /// <returns>Ширина и высота доски вместе с бордюром</returns>
        public Size GetDrawBoardSize()
        {
            var boardSize = _board.SideSize;
            return new Size(boardSize * CellSize + BorderWidth * 2, boardSize * CellSize + BorderWidth * 2);
        }

        /// <summary>
        /// Получение адреса клетки под курсором
        /// </summary>
        /// <param name="mouse">Координаты курсора</param>
        /// <returns>Возвращается адрес клетки</returns>
        public Address GetCellAddress(Point mouse)
        {
            var side = _game.Player == Player.Black; // переворот доски
            var boardSize = _board.SideSize;
            for (var i = 0; i < boardSize; i++)
            {
                var ix = side ? boardSize - i - 1 : i;
                for (var j = 0; j < boardSize; j++)
                {
                    var jx = side ? boardSize - j - 1 : j;
                    var rect = new Rectangle(_topLeftSize.Width + BorderWidth + jx * CellSize,
                        _topLeftSize.Height + BorderWidth + ix * CellSize, CellSize, CellSize);
                    if (rect.Contains(mouse))
                        return new Address(j, i);
                }
            }
            return new Address();
        }

        /// <summary>
        /// Получаем прямоугольник ячейки под курсором
        /// </summary>
        /// <param name="mouse"></param>
        /// <returns></returns>
        public Rectangle GetCellRect(Point mouse)
        {
            var side = _game.Player == Player.Black; // переворот доски
            var boardSize = _board.SideSize;
            for (var i = 0; i < boardSize; i++)
            {
                var ix = side ? boardSize - i - 1 : i;
                for (var j = 0; j < boardSize; j++)
                {
                    var jx = side ? boardSize - j - 1 : j;
                    var rect = new Rectangle(_topLeftSize.Width + BorderWidth + jx * CellSize,
                        _topLeftSize.Height + BorderWidth + ix * CellSize, CellSize, CellSize);
                    if (rect.Contains(mouse))
                        return rect;
                }
            }
            return Rectangle.Empty;
        }

        /// <summary>
        /// Получение клетки по координатам под курсором
        /// </summary>
        /// <param name="mouse">Координаты курсора</param>
        /// <param name="cell">Найденная клетка доски</param>
        /// <returns>true - клетка найдена под курсором</returns>
        public bool GetCell(Point mouse, out Cell cell)
        {
            cell = null;
            return _board.GetCell(GetCellAddress(mouse), out cell);
        }

        /// <summary>
        /// Рисование доски
        /// </summary>
        /// <param name="graphics">Канва для рисования</param>
        public void DrawBoard(Graphics graphics)
        {
            var side = _game.Player == Player.Black; // переворот доски
            var fields = _board.GetFields();
            var map = _board.GetMap();
            var cellsCount = _board.SideSize;
            var boardSize = GetDrawBoardSize();
            var boardRect = new Rectangle(new Point(_topLeftSize.Width, _topLeftSize.Height), boardSize);
            using (var brush = new SolidBrush(Color.FromArgb(234, 206, 175)))
                graphics.FillRectangle(brush, boardRect);
            DrawCharBorder(graphics, boardRect, cellsCount, side);
            DrawNumberBorder(graphics, boardRect, cellsCount, side);
            // рисуем поля доски
            for (var i = 0; i < cellsCount; i++)
            {
                var ix = side ? cellsCount - i - 1 : i;
                for (var j = 0; j < cellsCount; j++)
                {
                    var jx = side ? cellsCount - j - 1 : j;
                    var rect = new Rectangle(_topLeftSize.Width + BorderWidth + jx * CellSize,
                        _topLeftSize.Height + BorderWidth + ix * CellSize, CellSize, CellSize);
                    var address = new Address(j, i);
                    var fieldState = (State)fields[address]; // цвет поля доски
                    var mapCell = (Cell)map[address];        // наличие и цвет фигур
                    using (var brush = new SolidBrush(fieldState == State.Black
                                             ? Color.FromArgb(129, 112, 94)
                                             : Color.FromArgb(233, 217, 200)))
                        graphics.FillRectangle(_hoverCells.Contains(mapCell) && !_down
                                                    ? Brushes.DarkGray
                                                    : _board.GetSteps().Contains(mapCell) ? Brushes.DarkGray
                                                              : _board.GetBattles().Contains(mapCell) ? Brushes.DarkGray : brush, rect);
                    if (mapCell != _board.Selected || !_down)
                        DrawChecker(graphics, rect, mapCell);
                }
            }
            if (_down)
                DrawChecker(graphics, _moveRect, _board.Selected, true);
        }

        /// <summary>
        /// Рисуем шашку
        /// </summary>
        /// <param name="graphics">Канва</param>
        /// <param name="rect">Прямоугольник рисования</param>
        /// <param name="mapCell">Ссылка на ячейку с фишкой</param>
        /// <param name="shadow">Тень под фишкой</param>
        private static void DrawChecker(Graphics graphics, Rectangle rect, Cell mapCell, bool shadow = false)
        {
            if (mapCell == null) return;
            if (mapCell.State == State.White || mapCell.State == State.Black)
            {
                var sizeW = CellSize - (int)(CellSize * 0.91);
                var sizeH = CellSize - (int)(CellSize * 0.91);
                rect.Inflate(-sizeW, -sizeH);
                if (shadow)
                {
                    const int shadowOffset = 6;
                    rect.Offset(shadowOffset, shadowOffset);
                    using (var brush = new SolidBrush(Color.FromArgb(128, Color.Black)))
                        graphics.FillEllipse(brush, rect);
                    rect.Offset(-shadowOffset, -shadowOffset);
                }
                using (var brush = new SolidBrush(mapCell.State == State.Black
                                     ? Color.FromArgb(10, 10, 10)
                                     : Color.FromArgb(250, 250, 250)))
                    graphics.FillEllipse(brush, rect);
                rect.Inflate(-sizeW, -sizeH);
                graphics.DrawEllipse(Pens.Gray, rect);
                if (!mapCell.King)
                {
                    rect.Inflate(-sizeW, -sizeH);
                    graphics.DrawEllipse(Pens.Gray, rect);
                }
            }
        }

        #region Рисование бордюра вокруг доски с обозначениями полей

        /// <summary>
        /// Рисование букв столбцов по обеим сторонам доски
        /// </summary>
        /// <param name="graphics">Канва для рисования</param>
        /// <param name="boardRect">Размер доски вместе с рамкой</param>
        /// <param name="cellsCount">Количество клеток по стороне</param>
        /// <param name="side">Сторона игрока (нижняя): false - белые, true - чёрные</param>
        private void DrawCharBorder(Graphics graphics, Rectangle boardRect, int cellsCount, bool side)
        {
            var chars = side ? "HGFEDCBA" : "ABCDEFGH";
            for (var j = 0; j < cellsCount; j++)
            {
                var topRect = new Rectangle(BorderWidth + j * CellSize, _topLeftSize.Height, CellSize, BorderWidth);
                var bottomRect = new Rectangle(BorderWidth + j * CellSize,
                                           _topLeftSize.Height + boardRect.Height - BorderWidth, CellSize, BorderWidth);
                using (var sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    var ch = chars.ToCharArray()[j].ToString();
                    graphics.DrawString(ch, SystemFonts.CaptionFont, Brushes.Black, topRect, sf);
                    graphics.DrawString(ch, SystemFonts.CaptionFont, Brushes.Black, bottomRect, sf);
                }
            }
        }

        /// <summary>
        /// Рисование номеров строк по обеим сторонам доски
        /// </summary>
        /// <param name="graphics">Канва для рисования</param>
        /// <param name="boardRect">Размер доски вместе с рамкой</param>
        /// <param name="cellsCount">Количество клеток по стороне</param>
        /// <param name="side">Сторона игрока (нижняя): false - белые, true - чёрные</param>
        private void DrawNumberBorder(Graphics graphics, Rectangle boardRect, int cellsCount, bool side)
        {
            var chars = side ? "12345678" : "87654321";
            for (var i = 0; i < cellsCount; i++)
            {
                var topRect = new Rectangle(0, _topLeftSize.Height + BorderWidth + i * CellSize, BorderWidth, CellSize);
                var bottomRect = new Rectangle(boardRect.Width - BorderWidth,
                                               _topLeftSize.Height + BorderWidth + i * CellSize, BorderWidth, CellSize);
                using (var sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    var ch = chars.ToCharArray()[i].ToString();
                    graphics.DrawString(ch, SystemFonts.CaptionFont, Brushes.Black, topRect, sf);
                    graphics.DrawString(ch, SystemFonts.CaptionFont, Brushes.Black, bottomRect, sf);
                }
            }
        }

        #endregion

        bool _down;
        Point _downOffset;
        Rectangle _moveRect;


        /// <summary>
        /// Пользователь выбрал курсором ячейку или фишку
        /// </summary>
        /// <param name="location"></param>
        /// <param name="modifierKeys"></param>
        public void MouseDown(Point location)
        {
            var sourceAddress = GetCellAddress(location);
            _board.SelectSourceCell(sourceAddress);
            if (_board.Selected != null)
            {
                _down = true;
                _moveRect = GetCellRect(location);
                _moveRect.Offset(-2, -2); // "приподнимание" фишки в момент нажатия
                _downOffset = new Point(location.X - _moveRect.X, location.Y - _moveRect.Y);
            }
        }

        Cell _lastCell; // была выбрана в прошлый раз

        /// <summary>
        /// Перемещение указателя мыши
        /// </summary>
        /// <param name="location">Позиция курсора</param>
        public void MouseMove(Point location)
        {
            if (_game.WinPlayer != WinPlayer.Game) return;
            var address = GetCellAddress(location);
            Cell cell;
            // если под курсором найдена разрешённая ячейка
            if (GetCell(location, out cell) && cell.State != State.Prohibited)
            {
                if (_game.Mode == PlayMode.Game || _game.Mode == PlayMode.NetGame)
                {
                    if (_game.Player == Player.Black && !_game.Direction ||
                    _game.Player == Player.White && _game.Direction) return;
                }
                // и эта ячейка другая 
                if (cell != _lastCell)
                {
                    // если уже была выбрана другая ячейка
                    if (_lastCell != null) LeaveCell(_lastCell);
                    _lastCell = cell;   // запоминаем выбранную ячейку
                    // пытаемся выбрать эту ячейку
                    EnterCell(cell);
                }
            }
            else if (_lastCell != null)
            {
                // покидаем ячейку
                LeaveCell(_lastCell);
                _lastCell = null;
            }

            if (_down)
            {
                _moveRect.Location = new Point(location.X - _downOffset.X, location.Y - _downOffset.Y);
            }
        }

        public void MouseUp(Point location)
        {
            if (_down)
            {
                var targetAddress = GetCellAddress(location);
                Cell cell;
                // если под курсором найдена разрешённая ячейка
                if (GetCell(location, out cell) && cell != _board.Selected && cell.State != State.Prohibited)
                {
                    _board.SelectTargetCell(targetAddress);
                }
                else
                    _board.Selected = null;
                _down = false;
                _downOffset = Point.Empty;
                _moveRect = Rectangle.Empty;
            }
        }

        /// <summary>
        /// Попытка выбрать ячейку
        /// </summary>
        /// <param name="cell"></param>
        private void EnterCell(Cell cell)
        {
            _hoverCells.Clear();
            // добавляем, только если можно выбрать
            if (_board.CanCellEnter(cell))
                _hoverCells.Add(cell);
        }

        /// <summary>
        /// Действия при покидании ячейки
        /// </summary>
        /// <param name="cell"></param>
        private void LeaveCell(Cell cell)
        {
            _hoverCells.Clear();
        }

    }

}

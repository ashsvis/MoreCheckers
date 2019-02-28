using CheckersAppServer;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Checkers
{
    public class Io
    {
        const int CellSize = 50;    // размер стороны клетки в пикселах
        const int BorderWidth = 20; // ширина бордюра в пикселах
        Board _board;
        Game _game;
        List<Cell> _hoverCells;

        public Io(Game game, Board board)
        {
            _game = game;
            _board = board;
            _hoverCells = new List<Cell>();
        }

        public void SetGame(Game game) => _game = game;

        /// <summary>
        /// Получение расчётного размера доски
        /// </summary>
        /// <returns>Ширина и высота доски вместе с бордюром</returns>
        private Size GetDrawBoardSize()
        {
            var boardSize = _board.SideSize;
            return new Size(boardSize * CellSize + BorderWidth * 2, boardSize * CellSize + BorderWidth * 2);
        }

        /// <summary>
        /// Получение адреса клетки под курсором
        /// </summary>
        /// <param name="mouse">Координаты курсора</param>
        /// <returns>Возвращается адрес клетки</returns>
        private Address GetCellAddress(Point mouse)
        {
            var side = false; //_game.Player == Player.Black; // переворот доски
            var boardSize = _board.SideSize;
            for (var i = 0; i < boardSize; i++)
            {
                var ix = side ? boardSize - i - 1 : i;
                for (var j = 0; j < boardSize; j++)
                {
                    var jx = side ? boardSize - j - 1 : j;
                    var rect = new Rectangle(BorderWidth + jx * CellSize,
                                             BorderWidth + ix * CellSize, CellSize, CellSize);
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
        private Rectangle GetCellRect(Point mouse)
        {
            var side = false; //_game.Player == Player.Black; // переворот доски
            var boardSize = _board.SideSize;
            for (var i = 0; i < boardSize; i++)
            {
                var ix = side ? boardSize - i - 1 : i;
                for (var j = 0; j < boardSize; j++)
                {
                    var jx = side ? boardSize - j - 1 : j;
                    var rect = new Rectangle(BorderWidth + jx * CellSize,
                                             BorderWidth + ix * CellSize, CellSize, CellSize);
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
        private bool GetCell(Point mouse, out Cell cell)
        {
            cell = null;
            return _board.GetCell(GetCellAddress(mouse), out cell);
        }

        /// <summary>
        /// Генерация скрипта рисования доски и фишек
        /// </summary>
        /// <returns>Возвращается текст скрипта</returns>
        public string DrawBoardScript()
        {
            var sb = new StringBuilder();
            // закраска фона доски
            var fields = _board.GetFields();
            var map = _board.GetMap();
            var cellsCount = _board.SideSize;
            var boardSize = GetDrawBoardSize();
            var boardRect = new Rectangle(Point.Empty, boardSize);
            sb.AppendLine(string.Format("FillColor = {0};{1};{2}", 234, 206, 175));

            sb.AppendLine(string.Format("BoardRect = {0};{1};{2};{3}", boardRect.X, boardRect.Y, boardRect.Width, boardRect.Height));
            sb.AppendLine("rect BoardRect");

            sb.AppendLine("fill");
            DrawCharBorder(sb, boardRect, cellsCount);
            DrawNumberBorder(sb, boardRect, cellsCount);
            // рисуем поля доски
            for (var i = 0; i < cellsCount; i++)
            {
                for (var j = 0; j < cellsCount; j++)
                {
                    var rect = new Rectangle(BorderWidth + j * CellSize,
                                             BorderWidth + i * CellSize, CellSize, CellSize);
                    sb.AppendLine(string.Format("r2 = {0};{1};{2};{3}", rect.X, rect.Y, rect.Width, rect.Height));

                    var address = new Address(j, i);
                    var fieldState = (State)fields[address]; // цвет поля доски
                    var mapCell = (Cell)map[address];        // наличие и цвет фигур

                    if (fieldState == State.Black)
                        sb.AppendLine(string.Format("FillColor = {0};{1};{2}", 129, 112, 94));
                    else
                        sb.AppendLine(string.Format("FillColor = {0};{1};{2}", 233, 217, 200));
                    if (_hoverCells.Contains(mapCell) && !_down)
                        sb.AppendLine(string.Format("FillColor = {0};{1};{2}", 169, 169, 169));
                    else if (_board.GetSteps().Contains(mapCell))
                        sb.AppendLine(string.Format("FillColor = {0};{1};{2}", 169, 169, 169));
                    else if (_board.GetBattles().Contains(mapCell))
                        sb.AppendLine(string.Format("FillColor = {0};{1};{2}", 169, 169, 169));

                    sb.AppendLine("rect r2");
                    sb.AppendLine("fill");
                    if (mapCell != _board.Selected)
                        sb.Append(DrawCheckerScript(rect, mapCell));
                }
            }

            if (_down)
            {
                var rect = _moveRect;
                sb.Append(DrawCheckerScript(rect, _board.Selected, true));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Рисуем шашку
        /// </summary>
        /// <param name="sb">Накопитель текста</param>
        /// <param name="rect">Прямоугольник рисования</param>
        /// <param name="mapCell">Ссылка на ячейку с фишкой</param>
        /// <param name="shadow">Тень под фишкой</param>
        public static string DrawCheckerScript(Rectangle rect, Cell mapCell, bool shadow = false)
        {
            var sb = new StringBuilder();
            if (mapCell != null && (mapCell.State == State.White || mapCell.State == State.Black))
            {
                var sizeW = CellSize - (int)(CellSize * 0.91);
                var sizeH = CellSize - (int)(CellSize * 0.91);
                rect.Inflate(-sizeW, -sizeH);
                if (shadow)
                {
                    sb.AppendLine("FillOpacity = 128");
                    sb.AppendLine(string.Format("FillColor = {0};{1};{2}", 0, 0, 0));
                    sb.AppendLine("offset 6 6");
                    sb.AppendLine(string.Format("r1 = {0};{1};{2};{3}", rect.X, rect.Y, rect.Width, rect.Height));
                    sb.AppendLine("circle r1");
                    sb.AppendLine("fill");
                    sb.AppendLine("offset -6 -6");
                    sb.AppendLine("FillOpacity = 255");
                }
                if (mapCell.State == State.Black)
                    sb.AppendLine(string.Format("FillColor = {0};{1};{2}", 10, 10, 10));
                else
                    sb.AppendLine(string.Format("FillColor = {0};{1};{2}", 250, 250, 250));
                sb.AppendLine(string.Format("r1 = {0};{1};{2};{3}", rect.X, rect.Y, rect.Width, rect.Height));
                sb.AppendLine("circle r1");
                sb.AppendLine("fill");
                rect.Inflate(-sizeW, -sizeH);
                sb.AppendLine(string.Format("FillColor = {0};{1};{2}", 192, 192, 192));
                sb.AppendLine(string.Format("r2 = {0};{1};{2};{3}", rect.X, rect.Y, rect.Width, rect.Height));
                sb.AppendLine("circle r2");
                sb.AppendLine("draw");
                if (!mapCell.King)
                {
                    rect.Inflate(-sizeW, -sizeH);
                    sb.AppendLine(string.Format("r3 = {0};{1};{2};{3}", rect.X, rect.Y, rect.Width, rect.Height));
                    sb.AppendLine("circle r3");
                    sb.AppendLine("draw");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Рисование букв столбцов по обеим сторонам доски
        /// </summary>
        /// <param name="sb">Накопитель текста</param>
        /// <param name="boardRect">Размер доски вместе с рамкой</param>
        /// <param name="cellsCount">Количество клеток по стороне</param>
        /// <param name="side">Сторона игрока (нижняя): false - белые, true - чёрные</param>
        private void DrawCharBorder(StringBuilder sb, Rectangle boardRect, int cellsCount)
        {
            var chars = "ABCDEFGH";
            sb.AppendLine("FontName = Courier New");
            sb.AppendLine("FontSize = 12");
            sb.AppendLine("TextAlign = MiddleCenter");
            sb.AppendLine("FillOpacity = 255");
            sb.AppendLine(string.Format("FillColor = {0};{1};{2}", 0, 0, 0));
            for (var j = 0; j < cellsCount; j++)
            {
                var topRect = new RectangleF(BorderWidth + j * CellSize, 0, CellSize, BorderWidth);
                var bottomRect = new RectangleF(BorderWidth + j * CellSize,
                                               boardRect.Height - BorderWidth, CellSize, BorderWidth);
                sb.AppendLine(string.Format("m5 = {0};{1}", topRect.X, topRect.Y));
                sb.AppendLine(string.Format("m51 = {0};{1}", topRect.X + CellSize, topRect.Y));
                sb.AppendLine(string.Format("m52 = {0};{1}", topRect.X, topRect.Y + BorderWidth));
                sb.AppendLine(string.Format("m6 = {0};{1}", bottomRect.X, bottomRect.Y));
                sb.AppendLine(string.Format("m61 = {0};{1}", bottomRect.X + CellSize, bottomRect.Y));
                sb.AppendLine(string.Format("m62 = {0};{1}", bottomRect.X, bottomRect.Y + BorderWidth));
                var ch = chars.ToCharArray()[j].ToString();
                sb.AppendLine(string.Format("text m5 m51 m52 {0}",ch));
                sb.AppendLine("fill");
                sb.AppendLine(string.Format("text m6 m61 m62 {0}", ch));
                sb.AppendLine("fill");
            }
        }

        /// <summary>
        /// Рисование номеров строк по обеим сторонам доски
        /// </summary>
        /// <param name="sb">Накопитель текста</param>
        /// <param name="boardRect">Размер доски вместе с рамкой</param>
        /// <param name="cellsCount">Количество клеток по стороне</param>
        /// <param name="side">Сторона игрока (нижняя): false - белые, true - чёрные</param>
        private void DrawNumberBorder(StringBuilder sb, Rectangle boardRect, int cellsCount)
        {
            var chars = "87654321";
            sb.AppendLine("FontName = Courier New");
            sb.AppendLine("FontSize = 12");
            sb.AppendLine("TextAlign = MiddleCenter");
            sb.AppendLine("FillOpacity = 255");
            sb.AppendLine(string.Format("FillColor = {0};{1};{2}", 0, 0, 0));
            for (var i = 0; i < cellsCount; i++)
            {
                var topRect = new Rectangle(0, BorderWidth + i * CellSize, BorderWidth, CellSize);
                var bottomRect = new Rectangle(boardRect.Width - BorderWidth,
                                               BorderWidth + i * CellSize, BorderWidth, CellSize);
                sb.AppendLine(string.Format("m7 = {0};{1}", topRect.X, topRect.Y));
                sb.AppendLine(string.Format("m71 = {0};{1}", topRect.X + BorderWidth, topRect.Y));
                sb.AppendLine(string.Format("m72 = {0};{1}", topRect.X, topRect.Y + CellSize));
                sb.AppendLine(string.Format("m8 = {0};{1}", bottomRect.X, bottomRect.Y));
                sb.AppendLine(string.Format("m81 = {0};{1}", bottomRect.X + BorderWidth, bottomRect.Y));
                sb.AppendLine(string.Format("m82 = {0};{1}", bottomRect.X, bottomRect.Y + CellSize));
                var ch = chars.ToCharArray()[i].ToString();
                sb.AppendLine(string.Format("TEXT = {0}", ch));
                sb.AppendLine("text m7 m71 m72 TEXT");
                sb.AppendLine("fill");
                sb.AppendLine("text m8 m81 m82 TEXT");
                sb.AppendLine("fill");
            }
        }

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
            // если под курсором найдена разрешённая ячейка
            if (GetCell(location, out Cell cell) && cell.State != State.Prohibited)
            {
                if (_game.DisableNotOrderedMove()) return;
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
                _moveRect.Location = new Point(location.X - _downOffset.X, location.Y - _downOffset.Y);
        }

        public void MouseUp(Point location)
        {
            if (_down)
            {
                var targetAddress = GetCellAddress(location);
                // если под курсором найдена разрешённая ячейка
                if (GetCell(location, out Cell cell) && cell != _board.Selected && cell.State != State.Prohibited)
                    _board.SelectTargetCell(targetAddress);
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

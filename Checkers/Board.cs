using System;
using System.Collections;
using System.Collections.Generic;

namespace Checkers
{
    /// <summary>
    /// Результат хода
    /// </summary>
    public enum MoveResult
    {
        Prohibited,                 // запрещено
        SuccessfullMove,            // разрешённый простой ход
        SuccessfullCombat           // разрешённое взятие шашки противника
    }

    public enum GoalDirection
    {
        NW,
        N,
        NE,
        E,
        SE,
        S,
        SW,
        W
    }

    [Serializable]
    public class Board
    {
        const int _boardSize = 8;
        private readonly Hashtable _fields = new Hashtable();
        private Hashtable _cells = new Hashtable();
        private Cell _selected;

        private int _movedCount = 0;
        List<Cell> _steps = new List<Cell>();
        List<Cell> _battles = new List<Cell>();
        Game _game;

        public Board(Game game)
        {
            _game = game;
        }

        /// <summary>
        /// Привязка объекта game к доске
        /// </summary>
        /// <param name="game"></param>
        public void SetGame(Game game)
        {
            _game = game;
            var n = _game.Log.Count - 1;
            if (n < 0) return;
            var map = _game.Log[n].GetLastMap().DeepClone();
            SetMap(map);
        }

        public int SideSize { get { return _boardSize; } }

        /// <summary>
        /// Построение доски вначале
        /// </summary>
        public void ResetMap()
        {
            var whiteCheckers = "a1,c1,e1,g1,b2,d2,f2,h2,a3,c3,e3,g3";
            var blackCheckers = "b6,d6,f6,h6,a7,c7,e7,g7,b8,d8,f8,h8";
            _cells.Clear();
            _fields.Clear();
            _game.Direction = false;
            Selected = null;
            var black = false;  // признак чередования цветов полей
            for (var i = 0; i < _boardSize; i++)
            {
                State state;
                for (var j = 0; j < _boardSize; j++)
                {
                    var address = new Address(j, i);
                    if (whiteCheckers.Contains(address.ToString()))
                        state = State.White;
                    else if (blackCheckers.Contains(address.ToString()))
                        state = State.Black;
                    else
                        state = black ? State.Empty : State.Prohibited;
                    var cell = new Cell() { Address = address, State = state };
                    _cells[address] = cell;
                    _fields[address] = black ? State.Black : State.White;
                    black = !black; // чередуем цвет в столбце
                }
                black = !black;     // чередуем цвет в строке
            }
        }

        /// <summary>
        /// Положение фишек на доске в текстовую строку
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var list = new List<string>();
            foreach (var item in _cells.Values)
            {
                var cell = (Cell)item;
                switch (cell.State)
                {
                    case State.Black:
                        list.Add(string.Format("{0}{1}", cell.King ? "B" : "b", cell.Address));
                        break;
                    case State.White:
                        list.Add(string.Format("{0}{1}", cell.King ? "W" : "w", cell.Address));
                        break;
                }
            }
            return string.Join(",", list);
        }

        /// <summary>
        /// Установка фишек на доску из текстовой строки
        /// </summary>
        /// <param name="text"></param>
        public void SetMap(string text)
        {
            // очистка доски
            foreach (var item in _cells.Values)
            {
                var cell = (Cell)item;
                if (cell.State == State.Prohibited) continue;
                cell.King = false;
                cell.State = State.Empty;
            }
            // расстановка полученных фигур
            foreach (var value in text.Split(new[] { ',' }))
            {
                if (value.Length != 3) continue;
                var address = new Address(value.Substring(1));
                var cell = (Cell)_cells[address];
                switch (value[0])
                {
                    case 'b':
                        cell.State = State.Black;
                        break;
                    case 'B':
                        cell.State = State.Black;
                        cell.King = true;
                        break;
                    case 'w':
                        cell.State = State.White;
                        break;
                    case 'W':
                        cell.State = State.White;
                        cell.King = true;
                        break;
                }
            }
        }

        public Hashtable GetMap()
        {
            return _cells;
        }

        /// <summary>
        /// Установка фишек на доску из хеш-таблицы
        /// </summary>
        /// <param name="value"></param>
        public void SetMap(object value)
        {
            _cells = (Hashtable)value;
        }

        /// <summary>
        /// Получить таблицу полей доски
        /// </summary>
        /// <returns></returns>
        public Hashtable GetFields()
        {
            return _fields;
        }

        /// <summary>
        /// Текущая ячейка
        /// </summary>
        public Cell Selected
        {
            get { return _selected; }
            set
            {
                _steps.Clear();
                _battles.Clear();
                _selected = value;
                if (_selected != null)
                    FillGoalCells(_selected);
            }
        }

        /// <summary>
        /// При текущем направлении игры проверяем, если у стороны "бьющие" фишки
        /// </summary>
        /// <returns></returns>
        public bool HasAnyCombat()
        {
            var result = false;
            foreach (var item in _cells)
            {
                var cell = (Cell)((DictionaryEntry)item).Value;
                var cellState = cell.State;
                var cellAddress = cell.Address;
                if (cellState == State.Empty || cellState == State.Prohibited) continue;
                if ((_game.Direction && cellState == State.Black ||
                    !_game.Direction && cellState == State.White) && HasCombat(cellAddress))
                    return true;
            }
            return result;
        }

        /// <summary>
        /// Фишка на этой позиции может произвести "бой"
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool HasCombat(Address pos)
        {
            var result = false;
            if (pos.IsEmpty()) return result;
            var cell = (Cell)_cells[pos];
            // запрет хода для фишек не в свою очередь
            if (_game.Direction && cell.State != State.Black ||
                !_game.Direction && cell.State != State.White) return result;
            return HasGoals(cell, true);
        }

        /// <summary>
        /// Проверка возможности "хода" фишки
        /// </summary>
        /// <param name="startPos">Начальная позиция</param>
        /// <param name="endPos">Конечная позиция</param>
        /// <param name="direction">Направление проверки: false - фишки идут вверх, true - фишки идут вниз</param>
        /// <returns>Результат хода</returns>
        public MoveResult CheckMove(Address startPos, Address endPos, bool? king = null)
        {
            var result = MoveResult.Prohibited;
            if (startPos.IsEmpty() || endPos.IsEmpty()) return result;

            var startCellState = ((Cell)_cells[startPos]).State;
            if (startCellState != State.Empty)
            {
                // запрет хода для фишек не в свою очередь
                if (_game.Direction && startCellState != State.Black ||
                    !_game.Direction && startCellState != State.White) return result;
            }
            var dX = endPos.Coords.X - startPos.Coords.X;
            var dY = endPos.Coords.Y - startPos.Coords.Y;

            var startCellIsKing = king != null ? (bool)king : ((Cell)_cells[startPos]).King;
            var targetCellState = ((Cell)_cells[endPos]).State;
            if (targetCellState == State.Empty)
            {
                // проверка "боя"
                if (Math.Abs(dX) == 2 && Math.Abs(dY) == 2)
                {
                    // поиск "промежуточной" ячейки
                    var victimPos = new Address((startPos.Coords.X + endPos.Coords.X) / 2,
                                                (startPos.Coords.Y + endPos.Coords.Y) / 2);
                    var victimCellState = ((Cell)_cells[victimPos]).State;
                    // снимаем только фишку противника
                    result = targetCellState != victimCellState && startCellState != victimCellState
                                ? MoveResult.SuccessfullCombat : result;
                }
                else
                // проверка "хода"
                if (Math.Abs(dX) == 1 && dY == 1 && _game.Direction ||
                        Math.Abs(dX) == 1 && dY == -1 && !_game.Direction ||
                        // дамка "ходит" во все стороны
                        Math.Abs(dX) == 1 && Math.Abs(dY) == 1 && startCellIsKing)
                    result = MoveResult.SuccessfullMove;
            }
            return result;
        }

        /// <summary>
        /// Делаем "ход"
        /// </summary>
        /// <param name="startPos">Начальная позиция</param>
        /// <param name="endPos">Конечная позиция</param>
        /// <returns>результат хода</returns>
        public MoveResult MakeMove(Address startPos, Address endPos)
        {
            var moveResult = _steps.Contains((Cell)_cells[endPos]) ? MoveResult.SuccessfullMove : MoveResult.Prohibited;
            if (moveResult == MoveResult.SuccessfullMove)
            {
                if (!HasCombat(startPos))
                    Move(startPos, endPos);
                else
                {
                    OnShowError("Обязан рубить фишку противника", "Правило");
                    return MoveResult.Prohibited; // обязан брать шашку противника
                }
            }
            else
            {
                moveResult = _battles.Contains((Cell)_cells[endPos]) ? MoveResult.SuccessfullCombat : MoveResult.Prohibited;
                if (moveResult == MoveResult.SuccessfullCombat)
                {
                    Move(startPos, endPos);
                    // снятие фишек противника
                    var dx = Math.Sign(endPos.Coords.X - startPos.Coords.X);
                    var dy = Math.Sign(endPos.Coords.Y - startPos.Coords.Y);
                    var address = startPos;
                    while (true)
                    {
                        address = new Address(address.Coords.X + dx, address.Coords.Y + dy);
                        if (address.IsEmpty() || address.Coords == endPos.Coords) break;
                        Remove(address);
                    }
                }
            }
            return moveResult;
        }

        public event Action UpdateStatus = delegate { };

        private void OnUpdateStatus()
        {
            UpdateStatus();
        }

        public event Action<string, string> ShowError = delegate { };

        private void OnShowError(string message, string caption)
        {
            ShowError(message, caption);
        }

        public event Func<string, string, bool> AskQuestion = delegate { return false; };

        private bool OnAskQuestion(string message, string caption)
        {
            return AskQuestion(message, caption);
        }

        /// <summary>
        /// Переносим фишку на доске (вспомогательный метод)
        /// </summary>
        /// <param name="startPos">Начальная позиция</param>
        /// <param name="endPos">Конечная позиция</param>
        private void Move(Address startPos, Address endPos)
        {
            var startCell = (Cell)_cells[startPos];
            var endCell = (Cell)_cells[endPos];
            endCell.State = startCell.State;
            endCell.King = startCell.King;
            Remove(startPos);
        }

        /// <summary>
        /// Убираем фишку с доски (вспомогательный метод)
        /// </summary>
        /// <param name="pos">Позиция фишки</param>
        private void Remove(Address pos)
        {
            var cell = (Cell)_cells[pos];
            cell.State = State.Empty;
            cell.King = false;
        }

        /// <summary>
        /// Получение ячейки по адресу, с защитой от пустого адреса
        /// </summary>
        /// <param name="address"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        public bool GetCell(Address address, out Cell cell)
        {
            cell = null;
            if (address.IsEmpty()) return false;
            cell = (Cell)_cells[address];
            return cell != null;
        }

        /// <summary>
        /// Список ячеек, куда можно сделать ход
        /// </summary>
        /// <returns></returns>
        public List<Cell> GetSteps()
        {
            return _steps;
        }

        /// <summary>
        /// Список ячеек, куда можно сделать удар
        /// </summary>
        /// <returns></returns>
        public List<Cell> GetBattles()
        {
            return _battles;
        }

        public event Action<bool, Address, Address, MoveResult, int> CheckerMoved = delegate { };

        /// <summary>
        /// Обработка события перемещения фишки
        /// </summary>
        /// <param name="direction">Текущее направление игры: false - ходят белые</param>
        /// <param name="startPos">Начальное положение</param>
        /// <param name="endPos">Конечное положение</param>
        /// <param name="moveResult">Результат хода</param>
        private void OnCheckerMoved(bool direction, Address startPos, Address endPos, MoveResult moveResult, int stepCount)
        {
            CheckerMoved(direction, startPos, endPos, moveResult, stepCount);
        }

        public event Action ActivePlayerChanged = delegate { };

        private void OnActivePlayerChanged()
        {
            ActivePlayerChanged();
        }

        /// <summary>
        /// Выбираем фишку для начала хода
        /// </summary>
        /// <param name="location"></param>
        public void SelectSourceCell(Address location)
        {
            if (_game.WinPlayer != WinPlayer.Game) return;
            Cell cell;
            if (GetCell(location, out cell) && cell.State != State.Prohibited)
            {
                if (_game.Mode == PlayMode.Game || _game.Mode == PlayMode.NetGame)
                {
                    if (_game.Player == Player.Black && !_game.Direction ||
                        _game.Player == Player.White && _game.Direction) return;
                }
                // если ячейка не пустая, но не может быть выбрана
                if (cell.State != State.Empty && !CanCellEnter(cell))
                    return;
                // не должно быть выбранной ячейки с фишкой
                if (Selected == null)
                    // можем выбирать фишки только цвета игрока
                    SetSelectedCell(cell);
            }
        }

        /// <summary>
        /// Выбор целевой ячейки для хода или боя
        /// </summary>
        /// <param name="location"></param>
        public void SelectTargetCell(Address location)
        {
            if (_game.WinPlayer != WinPlayer.Game) return;
            Cell cell;
            if (GetCell(location, out cell) && cell.State != State.Prohibited)
            {
                if (_game.Mode == PlayMode.Game || _game.Mode == PlayMode.NetGame)
                {
                    if (_game.Player == Player.Black && !_game.Direction ||
                   _game.Player == Player.White && _game.Direction) return;
                }
                if (Selected != null && cell.State == State.Empty) // ранее была выбрана фишка и выбрана пустая клетка
                // пробуем делать ход
                {
                    var startPos = Selected.Address;
                    var endPos = cell.Address;
                    var moveResult = MakeMove(startPos, endPos);
                    Selected = null;  // после хода сбрасываем текущую выбранную фишку
                    if (moveResult != MoveResult.Prohibited)
                    {
                        // подсчёт очков
                        if (moveResult == MoveResult.SuccessfullCombat)
                        {
                            if (_game.Direction)
                                _game.BlackScore++;
                            else
                                _game.WhiteScore++;
                        }
                        _game.CheckWin();
                        // считаем количество непрерывных ходов одной стороной
                        _movedCount++;
                        // определение дамки
                        if (!cell.King &&
                            (!_game.Direction && cell.Address.Row == SideSize ||
                             _game.Direction && cell.Address.Row == 1)) cell.King = true;
                        var hasCombat = HasCombat(endPos); // есть ли в этой позиции возможность боя
                        // запоминаем очередь хода перед возможной сменой
                        var lastDirection = _game.Direction;
                        // или был бой и далее нет возможности боя
                        if (moveResult == MoveResult.SuccessfullCombat && !hasCombat ||
                            moveResult == MoveResult.SuccessfullMove)
                        {
                            // сообщаем о перемещении фишки
                            OnCheckerMoved(lastDirection, startPos, endPos, moveResult, _movedCount);
                            // сбрасываем количество непрерывных ходов одной стороной
                            _movedCount = 0;
                            // передача очерёдности хода
                            _game.Direction = !_game.Direction;
                            OnActivePlayerChanged();
                            _game.CheckWin();
                            if (_game.WinPlayer == WinPlayer.None)
                                CheckAvailableGoals();
                            return;
                        }
                        else if (moveResult == MoveResult.SuccessfullCombat && hasCombat)
                            // выбрана фишка для продолжения боя
                            SetSelectedCell(cell);
                        // сообщаем о перемещении фишки
                        OnCheckerMoved(lastDirection, startPos, endPos, moveResult, _movedCount);
                    }
                }
            }
        }

        private bool CheckAvailableGoals()
        {
            var player = _game.Player;
            foreach (var item in _cells.Values)
            {
                var cell = (Cell)item;
                if (player == Player.White && cell.State == State.White ||
                    player == Player.Black && cell.State == State.Black)
                {
                    if (HasGoals(cell)) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Выбираем только "игровые" клетки, по которым фишки двигаются
        /// </summary>
        /// <param name="cell">Выбранная ячейка</param>
        private void SetSelectedCell(Cell cell)
        {
            if (cell.State == State.Black || cell.State == State.White)
            {
                Selected = cell;
            }
        }

        /// <summary>
        /// Проверка возможности выбрать указанную ячейку для начала "хода"
        /// </summary>
        /// <param name="cell">Целевая ячейка</param>
        /// <returns>true - выбор возможен</returns>
        public bool CanCellEnter(Cell cell)
        {
            // пустую ячейку сделать текущей не можем
            if (cell.State == State.Empty) return false;
            // "ходят" чёрные и пытаемся выбрать белую фишку
            // "ходят" белые и пытаемся выбрать чёрную фишку
            if (_game.Direction && cell.State == State.White ||
                !_game.Direction && cell.State == State.Black) return false;
            // у фишки нет ходов
            if (!HasGoals(cell)) return false;
            // если по очереди некоторые фишки могут "ударить", но эта фишка "ударить" не может
            if (HasAnyCombat() && !HasCombat(cell.Address)) return false;
            OnUpdateStatus();
            return true;
        }

        /// <summary>
        /// Есть ли у фишки вообще ходы?
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private bool HasGoals(Cell cell, bool combat = false)
        {
            var steps = new List<Cell>();
            var battles = new List<Cell>();
            FillGoalCells(cell, steps, battles);
            var result = combat
                ? battles.Count > 0
                : steps.Count > 0 || battles.Count > 0;
            return result;
        }

        /// <summary>
        /// Заполнение списка ячеек, куда возможно перемещение фишки, с учетом правил
        /// </summary>
        /// <param name="selectedCell">Текущая ячейка с фишкой</param>
        public void FillGoalCells(Cell selectedCell, List<Cell> steps = null, List<Cell> battles = null)
        {
            if (steps == null) steps = _steps;
            if (battles == null) battles = _battles;
            var pos = selectedCell.Address;
            if (selectedCell.King) // дамка
            {
                AddKingGoal(steps, battles, pos, GoalDirection.NW);
                AddKingGoal(steps, battles, pos, GoalDirection.NE);
                AddKingGoal(steps, battles, pos, GoalDirection.SE);
                AddKingGoal(steps, battles, pos, GoalDirection.SW);
                if (battles.Count > 0)
                    steps.Clear();
            }
            else // обычная шашка
            {
                AddGoal(battles, pos, -2, -2);
                AddGoal(battles, pos, +2, -2);
                AddGoal(battles, pos, -2, +2);
                AddGoal(battles, pos, +2, +2);
                if (battles.Count > 0)
                    return;
                AddGoal(steps, pos, -1, -1);
                AddGoal(steps, pos, +1, -1);
                AddGoal(steps, pos, -1, +1);
                AddGoal(steps, pos, +1, +1);
            }
        }

        /// <summary>
        /// Добавление целевого поля для дамки
        /// </summary>
        /// <param name="goalList">Список целей, накопительный</param>
        /// <param name="pos">Адрес ячейки, вокруг которой ищется цель</param>
        /// <param name="direction">Направление поиска в "глубину"</param>
        /// <returns>true - была также найдена возможность боя</returns>
        private void AddKingGoal(List<Cell> steps, List<Cell> battles, Address pos, GoalDirection direction)
        {
            int dx, dy;
            switch (direction)
            {
                case GoalDirection.NW: dx = dy = -1; break;
                case GoalDirection.NE: dx = +1; dy = -1; break;
                case GoalDirection.SE: dx = dy = +1; break;
                case GoalDirection.SW: dx = -1; dy = +1; break;
                default: return;
            }
            var source = (Cell)_cells[pos];
            var combat = false;
            var addr = pos;
            while (true)
            {
                addr = new Address(addr.Coords.X + dx, addr.Coords.Y + dy);
                if (addr.IsEmpty()) break;
                var cell = (Cell)_cells[addr];
                if (cell.State == State.Empty)
                {
                    if (combat)
                        battles.Add((Cell)_cells[addr]);
                    else
                        steps.Add((Cell)_cells[addr]);
                }
                else if (cell.State != source.State)
                {
                    addr = new Address(addr.Coords.X + dx, addr.Coords.Y + dy);
                    if (addr.IsEmpty()) break;
                    cell = (Cell)_cells[addr];
                    if (cell.State == State.Empty)
                    {
                        if (combat) break;
                        battles.Add((Cell)_cells[addr]);
                        combat = true;
                    }
                    else
                        break;
                }
                else
                    break;
            }
        }

        /// <summary>
        /// Добавление целевого поля для шашки
        /// </summary>
        /// <param name="goalList">Список целей, накопительный</param>
        /// <param name="pos">Адрес ячейки, вокруг которой ищется цель</param>
        /// <param name="dx">Шаг поиска по горизонтали</param>
        /// <param name="dy">Шаг поиска по вертикали</param>
        private void AddGoal(List<Cell> goalList, Address pos, int dx, int dy)
        {
            var addr = new Address(pos.Coords.X + dx, pos.Coords.Y + dy);
            var check = CheckMove(pos, addr);
            if (check != MoveResult.Prohibited)
                goalList.Add((Cell)_cells[addr]);
        }
    }
}

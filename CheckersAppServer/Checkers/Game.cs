using CheckersAppServer;
using System;
using System.Collections.Generic;

namespace Checkers
{
    [Serializable]
    public class Game
    {
        public List<LogItem> Log { get; set; }

        public int WhiteScore { get; set; }
        public int BlackScore { get; set; }

        public bool Direction { get; set; }

        public Player Player { get; set; }

        public string PlayerName { get; set; }

        public WinPlayer WinPlayer { get; set; }

        public PlayMode Mode { get; set; }

        public Board Board { get; set; }

        public Io Io { get; set; }

        public Guid Id { get; set; }

        private List<string> _avalilableMoveCells;

        public List<string> AvalilableMoveCells
        {
            get { return _avalilableMoveCells; }
            set
            {
                _avalilableMoveCells.Clear();
                _avalilableMoveCells.AddRange(value);
            }
        }

        public Dictionary<string, string[]> AvalilableAnswerMoveCells { get; set; }

        public Game()
        {
            Id = Guid.NewGuid();
            Log = new List<LogItem>();
            _avalilableMoveCells = new List<string>();
            AvalilableAnswerMoveCells = new Dictionary<string, string[]>();
            Board = new Board(this);
            Board.CheckerMoved += Board_CheckerMoved;
            Board.ResetMap();
            Io = new Io(this, Board);
        }

        private void Board_CheckerMoved(bool direction, Address startPos, Address endPos, 
            MoveResult moveResult, int stepCount) => UpdateLog(direction, startPos, endPos, moveResult, stepCount);

        private void UpdateLog(bool direction, Address startPos, Address endPos, MoveResult moveResult, int stepCount)
        {
            var result = string.Format("{0}{1}{2}",
                    startPos, moveResult == MoveResult.SuccessfullCombat ? ":" : "-", endPos);
            if (!direction)
            {
                // ходят "белые"
                if (stepCount == 1) // первый ход (из, возможно, серии ходов)
                {
                    var item = new LogItem() { Number = Log.Count + 1, White = result };
                    item.AddToMap(Board.GetMap().DeepClone());
                    Log.Add(item);
                }
                else
                {
                    var item = Log[Log.Count - 1];
                    item.White += ":" + endPos;
                    item.AddToMap(Board.GetMap().DeepClone());
                }
            }
            else
            {
                // ходят "чёрные"
                var item = Log[Log.Count - 1];
                if (stepCount == 1) // первый ход (из, возможно, серии ходов)
                    item.Black = result;
                else
                    item.Black += ":" + endPos;
                item.AddToMap(Board.GetMap().DeepClone());
            }
        }

        /// <summary>
        /// Проверка выигрыша стороны
        /// </summary>
        /// <param name="availableMoves">Количество доступных ходов</param>
        /// <param name="direction">true - ход чёрных, false - ход белых</param>
        public void CheckWin(int availableMoves, bool direction)
        {
            WinPlayer = WhiteScore == 12
                ? WinPlayer.White
                : BlackScore == 12
                     ? WinPlayer.Black
                     : availableMoves == 0 
                         ? WhiteScore == BlackScore 
                                ? WinPlayer.Draw 
                                : direction ? WinPlayer.Black : WinPlayer.White 
                                            :  WinPlayer.Game;
        }

        public bool DisableNotOrderedMove()
        {
            if (Mode == PlayMode.Game || Mode == PlayMode.NetGame)
            {
                if (Player == Player.Black && !Direction ||
                    Player == Player.White && Direction) return true;
            }
            return false;
        }

        public override string ToString()
        {
            switch (WinPlayer)
            {
                case WinPlayer.None:
                    return "Выберите новую игру...";
                case WinPlayer.Wait:
                    return "Ожидание соперника...";
                case WinPlayer.Game:
                    return Direction ? "Ход чёрных..." : "Ход белых...";
                case WinPlayer.White:
                    return "Белые выиграли";
                case WinPlayer.Black:
                    return "Чёрные выиграли";
                case WinPlayer.Draw:
                    return "Ничья";
            }
            return base.ToString();
        }
    }
}

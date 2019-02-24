using System;
using System.Collections.Generic;

namespace Checkers
{
    public enum Player
    {
        White,
        Black
    }

    public enum WinPlayer
    {
        None,   // игра не начата
        Game,   // игра идёт
        White,  // белые выиграли
        Black,  // чёрные выиграли
        Draw    // ничья
    }

    public enum PlayMode
    {
        Game,       // игра с компьютером
        NetGame,    // игра по сети
        SelfGame,   // игра с самим собой
        Collocation // расстановка фишек
    }

    [Serializable]
    public class Game
    {
        public List<LogItem> Log { get; set; }

        public int WhiteScore { get; set; }
        public int BlackScore { get; set; }

        public bool Direction { get; set; }

        public Player Player { get; set; }

        public WinPlayer WinPlayer { get; set; }

        public PlayMode Mode { get; set; }

        public Board Board { get; set; }

        public Io Io { get; set; }

        public Game()
        {
            //Mode = PlayMode.SelfGame;
            //WinPlayer = WinPlayer.Game;
            Log = new List<LogItem>();
            Board = new Board(this);
            Board.CheckerMoved += Board_CheckerMoved;
            Board.ResetMap();
            Io = new Io(this, Board);
        }

        private void Board_CheckerMoved(bool direction, Address startPos, Address endPos, MoveResult moveResult, int stepCount)
        {
            UpdateLog(direction, startPos, endPos, moveResult, stepCount);
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

        public void CheckWin()
        {
            WinPlayer = WhiteScore == 12
                ? WinPlayer.White
                : BlackScore == 12
                     ? WinPlayer.Black
                     : WinPlayer.Game;
        }

        public override string ToString()
        {
            switch (WinPlayer)
            {
                case WinPlayer.None:
                    return "Выберите тип игры...";
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

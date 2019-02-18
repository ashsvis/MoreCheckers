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

        public Game()
        {
            Log = new List<LogItem>();
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

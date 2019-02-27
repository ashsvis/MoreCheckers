namespace CheckersAppServer
{
    public enum PlayMode
    {
        Game,       // игра с компьютером
        NetGame,    // игра по сети
        SelfGame,   // игра с самим собой
        Collocation // расстановка фишек
    }

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


}

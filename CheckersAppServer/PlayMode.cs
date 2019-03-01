namespace CheckersAppServer
{
    public enum PlayMode
    {
        None,       // нет статуса
        Game,       // игра с компьютером
        NetGame,    // игра по сети
        TestGame,   // игра с самим собой
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

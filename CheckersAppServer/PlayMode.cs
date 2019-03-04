namespace CheckersAppServer
{
    /// <summary>
    /// None,Game,NetGame,TestGame,Collocation
    /// </summary>
    public enum PlayMode
    {
        None,       // нет статуса
        Game,       // игра с компьютером
        NetGame,    // игра по сети
        TestGame,   // игра с самим собой
        Collocation // расстановка фишек
    }

    /// <summary>
    /// White,Black
    /// </summary>
    public enum Player
    {
        White,
        Black
    }

    /// <summary>
    /// None,Wait,Game,White,Black,Draw
    /// </summary>
    public enum WinPlayer
    {
        None,   // игра не начата
        Wait,   // ожидание игры
        Game,   // игра идёт
        White,  // белые выиграли
        Black,  // чёрные выиграли
        Draw    // ничья
    }


}

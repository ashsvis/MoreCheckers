using Checkers.CheckersServiceReference;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Checkers
{
    public static partial class Client
    {
        public static Guid CreateGame()
        {
            return GetMethod("CreateGame", () => _proxy.CreateGame(), Guid.Empty);
        }

        public static Task<Guid> CreateGameAsync()
        {
            var task = new Task<Guid>(() => CreateGame());
            task.Start();
            return task;
        }

        public static void DestroyGame(Guid gameId)
        {
            GetMethod("DestroyGame", () => _proxy.DestroyGame(gameId), false);
        }

        public static Guid[] GetActiveGames()
        {
            return GetArrayMethod("GetActiveGames", () => _proxy.GetActiveGames(), new Guid[] { });
        }

        public static Task<Guid[]> GetActiveGamesAsync()
        {
            var task = new Task<Guid[]>(() => GetActiveGames());
            task.Start();
            return task;
        }

        public static GameStatus GetGameStatus(Guid gameId)
        {
            var status = new GameStatus() { Exists = false, Text = "Ошибка вызова метода GetGameStatus" };
            return GetMethod("GetGameStatus", () => _proxy.GetGameStatus(gameId), status);
        }

        public static Task<GameStatus> GetGameStatusAsync(Guid gameId)
        {
            var task = new Task<GameStatus>(() => GetGameStatus(gameId));
            task.Start();
            return task;
        }

        public static string GetDrawBoardScript(Guid gameId, Player player)
        {
            return GetMethod("GetDrawBoardScript", () => _proxy.GetDrawBoardScript(gameId, player), String.Empty);
        }

        public static Task<string> GetDrawBoardScriptAsync(Guid gameId, Player player)
        {
            var task = new Task<string>(() => GetDrawBoardScript(gameId, player));
            task.Start();
            return task;
        }

        public static bool OnBoardMouseDown(Guid gameId, Point location, int modifierKeys, Player player)
        {
            return GetMethod("OnBoardMouseDown", () => _proxy.OnBoardMouseDown(gameId, location, modifierKeys, player), false);
        }

        public static Task<bool> OnBoardMouseDownAsync(Guid gameId, Point location, int modifierKeys, Player player)
        {
            var task = new Task<bool>(() => OnBoardMouseDown(gameId, location, modifierKeys, player));
            task.Start();
            return task;
        }

        public static bool OnBoardMouseMove(Guid gameId, Point location, int modifierKeys, Player player)
        {
            return GetMethod("OnBoardMouseMove", () => _proxy.OnBoardMouseMove(gameId, location, modifierKeys, player), false);
        }

        public static Task<bool> OnBoardMouseMoveAsync(Guid gameId, Point location, int modifierKeys, Player player)
        {
            var task = new Task<bool>(() => OnBoardMouseMove(gameId, location, modifierKeys, player));
            task.Start();
            return task;
        }

        public static bool OnBoardMouseUp(Guid gameId, Point location, int modifierKeys, Player player)
        {
            return GetMethod("OnBoardMouseUp", () => _proxy.OnBoardMouseUp(gameId, location, modifierKeys, player), false);
        }

        public static Task<bool> OnBoardMouseUpAsync(Guid gameId, Point location, int modifierKeys, Player player)
        {
            var task = new Task<bool>(() => OnBoardMouseUp(gameId, location, modifierKeys, player));
            task.Start();
            return task;
        }

        public static bool StartNewGame(Guid gameId, PlayMode playMode, Player player, string playerName)
        {
            return GetMethod("StartNewGame", () => _proxy.StartNewGame(gameId, playMode, player, playerName), false);
        }

        public static Task<bool> StartNewGameAsync(Guid gameId, PlayMode playMode, Player player, string playerName)
        {
            var task = new Task<bool>(() => StartNewGame(gameId, playMode, player, playerName));
            task.Start();
            return task;
        }

        public static bool JoinNewGame(Guid gameId, string playerName)
        {
            return GetMethod("JoinNewGame", () => _proxy.JoinNewGame(gameId, playerName), false);
        }

        public static Task<bool> JoinNewGameAsync(Guid gameId, string playerName)
        {
            var task = new Task<bool>(() => JoinNewGame(gameId, playerName));
            task.Start();
            return task;
        }

        public static bool EndGame(Guid gameId)
        {
            return GetMethod("EndGame", () => _proxy.EndGame(gameId), false);
        }

        public static Task<bool> EndGameAsync(Guid gameId)
        {
            var task = new Task<bool>(() => EndGame(gameId));
            task.Start();
            return task;
        }

    }
}

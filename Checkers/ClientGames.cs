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

        public static string GetGameStatus(Guid gameId)
        {
            return GetMethod("GetGameStatus", () => _proxy.GetGameStatus(gameId), String.Empty);
        }

        public static Task<string> GetGameStatusAsync(Guid gameId)
        {
            var task = new Task<string>(() => GetGameStatus(gameId));
            task.Start();
            return task;
        }

        public static string GetDrawBoardScript(Guid gameId)
        {
            return GetMethod("GetDrawBoardScript", () => _proxy.GetDrawBoardScript(gameId), String.Empty);
        }

        public static Task<string> GetDrawBoardScriptAsync(Guid gameId)
        {
            var task = new Task<string>(() => GetDrawBoardScript(gameId));
            task.Start();
            return task;
        }

        public static bool OnBoardMouseDown(Guid gameId, Point location, int modifierKeys)
        {
            return GetMethod("OnBoardMouseDown", () => _proxy.OnBoardMouseDown(gameId, location, modifierKeys), false);
        }

        public static Task<bool> OnBoardMouseDownAsync(Guid gameId, Point location, int modifierKeys)
        {
            var task = new Task<bool>(() => OnBoardMouseDown(gameId, location, modifierKeys));
            task.Start();
            return task;
        }

        public static bool OnBoardMouseMove(Guid gameId, Point location, int modifierKeys)
        {
            return GetMethod("OnBoardMouseMove", () => _proxy.OnBoardMouseMove(gameId, location, modifierKeys), false);
        }

        public static Task<bool> OnBoardMouseMoveAsync(Guid gameId, Point location, int modifierKeys)
        {
            var task = new Task<bool>(() => OnBoardMouseMove(gameId, location, modifierKeys));
            task.Start();
            return task;
        }

        public static bool OnBoardMouseUp(Guid gameId, Point location, int modifierKeys)
        {
            return GetMethod("OnBoardMouseUp", () => _proxy.OnBoardMouseUp(gameId, location, modifierKeys), false);
        }

        public static Task<bool> OnBoardMouseUpAsync(Guid gameId, Point location, int modifierKeys)
        {
            var task = new Task<bool>(() => OnBoardMouseUp(gameId, location, modifierKeys));
            task.Start();
            return task;
        }

        public static bool StartNewGame(Guid gameId, PlayMode playMode, Player player)
        {
            return GetMethod("StartNewGame", () => _proxy.StartNewGame(gameId, playMode, player), false);
        }

        public static Task<bool> StartNewGameAsync(Guid gameId, PlayMode playMode, Player player)
        {
            var task = new Task<bool>(() => StartNewGame(gameId, playMode, player));
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

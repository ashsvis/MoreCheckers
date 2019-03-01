using Checkers;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace CheckersAppServer
{
    public partial class CheckersService : ICheckersService
    {
        private static Dictionary<Guid, Game> _games = new Dictionary<Guid, Game>();

        public Guid CreateGame()
        {
            var game = new Game();
            var guid = game.Id;
            _games.Add(guid, game);
            Console.WriteLine($"Создание игры {guid}, тип игры: {game.Mode}");
            return guid;
        }

        public bool DestroyGame(Guid gameId)
        {
            if (!_games.ContainsKey(gameId)) return false;
            var gameMode = _games[gameId].Mode;
            _games.Remove(gameId);
            Console.WriteLine($"Удаление игры {gameId}, тип игры: {gameMode}");
            return true;
        }

        public Guid[] GetActiveGames()
        {
            var list = new List<Guid>();
            foreach (var gameId in _games.Keys)
            {
                if (!_games.ContainsKey(gameId)) continue;
                var game = _games[gameId];
                if (game.WinPlayer == WinPlayer.Game)
                    list.AddRange(_games.Keys);
            }
            return list.ToArray();
        }

        public GameStatus GetGameStatus(Guid gameId)
        {
            return CreateGameStatus(gameId);
        }

        private static GameStatus CreateGameStatus(Guid gameId)
        {
            var status = new GameStatus();
            if (!_games.ContainsKey(gameId))
            {
                status.Exists = false;
                status.Text = "Сессия потеряна...";
                status.WinPlayer = WinPlayer.None;
                return status;
            }
            var game = _games[gameId];
            status.Exists = true;
            status.Text = game.ToString();
            status.WinPlayer = game.WinPlayer;
            status.BlackScore = game.BlackScore;
            status.WhiteScore = game.WhiteScore;
            status.Log = new List<LogItem>(game.Log);
            return status;
        }

        public bool StartNewGame(Guid gameId, PlayMode gameType)
        {
            if (!_games.ContainsKey(gameId)) return false;
            var game = _games[gameId];
            game.Board.ResetMap(false);
            game.Mode = gameType;
            game.Player = Player.White;
            game.WinPlayer = WinPlayer.Game;
            Console.WriteLine($"Начало игры {gameId}, тип игры: {gameType}");
            return true;
        }

        public bool EndGame(Guid gameId)
        {
            if (!_games.ContainsKey(gameId)) return false;
            var game = _games[gameId];
            game.Board.ResetMap(true);
            Console.WriteLine($"Конец игры {gameId}, тип игры: {game.Mode}");
            return true;
        }

        public string GetDrawBoardScript(Guid gameId, Player player)
        {
            if (!_games.ContainsKey(gameId)) return "";
            var game = _games[gameId];
            game.Player = player;
            return game.Io.DrawBoardScript();
        }

        public bool OnBoardMouseDown(Guid gameId, Point location, int modifierKeys, Player player)
        {
            if (!_games.ContainsKey(gameId)) return false;
            var keys = (System.Windows.Forms.Keys)modifierKeys;
            var game = _games[gameId];
            game.Player = player;
            game.Io.MouseDown(location);
            return true;
        }

        public bool OnBoardMouseMove(Guid gameId, Point location, int modifierKeys, Player player)
        {
            if (!_games.ContainsKey(gameId)) return false;
            var keys = (System.Windows.Forms.Keys)modifierKeys;
            var game = _games[gameId];
            game.Player = player;
            game.Io.MouseMove(location);
            return true;
        }

        public bool OnBoardMouseUp(Guid gameId, Point location, int modifierKeys, Player player)
        {
            if (!_games.ContainsKey(gameId)) return false;
            var keys = (System.Windows.Forms.Keys)modifierKeys;
            var game = _games[gameId];
            game.Player = player;
            game.Io.MouseUp(location);
            return true;
        }

    }
}

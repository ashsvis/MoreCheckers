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
            var guid = Guid.NewGuid();
            _games.Add(guid, game);
            return guid;
        }

        public bool DestroyGame(Guid gameId)
        {
            if (!_games.ContainsKey(gameId)) return false;
            _games.Remove(gameId);
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

        public string GetGameStatus(Guid gameId)
        {
            if (!_games.ContainsKey(gameId)) return "Not found";
            var game = _games[gameId];
            return game.ToString();
        }

        public bool StartNewGame(Guid gameId, PlayMode gameType, Player player)
        {
            if (!_games.ContainsKey(gameId)) return false;
            var game = _games[gameId];
            game.Board.ResetMap(false);
            game.Mode = gameType;
            game.Player = player;
            game.WinPlayer = WinPlayer.Game;
            return true;
        }

        public bool EndGame(Guid gameId)
        {
            if (!_games.ContainsKey(gameId)) return false;
            var game = _games[gameId];
            game.Board.ResetMap(true);
            game.WinPlayer = WinPlayer.None;
            return true;
        }

        public string GetDrawBoardScript(Guid gameId)
        {
            return _games.ContainsKey(gameId) ? _games[gameId].Io.DrawBoardScript() : "";
        }

        public bool OnBoardMouseDown(Guid gameId, Point location, int modifierKeys)
        {
            if (!_games.ContainsKey(gameId)) return false;
            var keys = (System.Windows.Forms.Keys)modifierKeys;
            _games[gameId].Io.MouseDown(location);
            return true;
        }

        public bool OnBoardMouseMove(Guid gameId, Point location, int modifierKeys)
        {
            if (!_games.ContainsKey(gameId)) return false;
            var keys = (System.Windows.Forms.Keys)modifierKeys;
            _games[gameId].Io.MouseMove(location);
            return true;
        }

        public bool OnBoardMouseUp(Guid gameId, Point location, int modifierKeys)
        {
            if (!_games.ContainsKey(gameId)) return false;
            var keys = (System.Windows.Forms.Keys)modifierKeys;
            _games[gameId].Io.MouseUp(location);
            return true;
        }
    }
}

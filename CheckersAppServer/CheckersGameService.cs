using Checkers;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace CheckersAppServer
{
    public partial class CheckersService : ICheckersService
    {
        private Dictionary<Guid, Game> _games = new Dictionary<Guid, Game>();

        public Guid CreateGame()
        {
            var game = new Game();
            var guid = Guid.NewGuid();
            _games.Add(guid, game);
            return guid;
        }

        public bool StartNewGame(Guid gameId)
        {
            if (!_games.ContainsKey(gameId)) return false;
            var game = _games[gameId];
            game.Board.ResetMap(false);
            game.Mode = PlayMode.SelfGame;
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

using System;
using System.Drawing;
using System.ServiceModel;

namespace CheckersAppServer
{
    [ServiceContract]
    public interface ICheckersService
    {
        #region Работа с пользователем

        [OperationContract]
        string GetUserPasswordHash(string id);

        [OperationContract]
        string[] GetUserNames();

        [OperationContract]
        string[] GetUserProperties(string id);

        [OperationContract]
        bool AddUser(string fullname, string position, string sector, string hash);

        [OperationContract]
        bool ChangeUser(string id, string fullname, string position, string sector, string hash);

        [OperationContract]
        bool DeleteUser(string id);

        #endregion

        #region Работа с игрой

        [OperationContract]
        Guid CreateGame();

        [OperationContract]
        bool DestroyGame(Guid gameId);

        [OperationContract]
        Guid[] GetActiveGames();

        [OperationContract]
        string GetGameStatus(Guid gameId);

        [OperationContract]
        bool StartNewGame(Guid gameId, PlayMode gameType, Player player);

        [OperationContract]
        bool EndGame(Guid gameId);

        [OperationContract]
        string GetDrawBoardScript(Guid gameId);

        [OperationContract]
        bool OnBoardMouseDown(Guid gameId, Point location, int modifierKeys);

        [OperationContract]
        bool OnBoardMouseMove(Guid gameId, Point location, int modifierKeys);

        [OperationContract]
        bool OnBoardMouseUp(Guid gameId, Point location, int modifierKeys);

        #endregion

        [OperationContract]
        DateTime GetDate();
    }
}

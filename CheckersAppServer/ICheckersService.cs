using System;
using System.Drawing;
using System.ServiceModel;

namespace CheckersAppServer
{
    [ServiceContract(CallbackContract = typeof(IClientCallback))]
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
        GameStatus GetGameStatus(Guid gameId);

        [OperationContract]
        bool StartNewGame(Guid gameId, PlayMode gameType, Player player, string playerName);

        [OperationContract]
        bool JoinNewGame(Guid gameId, string playerName);

        [OperationContract]
        bool EndGame(Guid gameId);

        [OperationContract]
        string GetDrawBoardScript(Guid gameId, Player player);

        [OperationContract]
        bool OnBoardMouseDown(Guid gameId, Point location, int modifierKeys, Player player);

        [OperationContract]
        bool OnBoardMouseMove(Guid gameId, Point location, int modifierKeys, Player player);

        [OperationContract]
        bool OnBoardMouseUp(Guid gameId, Point location, int modifierKeys, Player player);

        #endregion

        [OperationContract]
        bool RegisterForUpdates(Guid clientId, Player player);

        [OperationContract(IsOneWay = true)]
        void UpdateGame(Guid clientId, Guid gameId);

        [OperationContract(IsOneWay = true)]
        void Disconnect(Guid clientId);

        [OperationContract]
        DateTime GetDate();
    }

}

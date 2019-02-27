using System;
using System.ServiceModel;

namespace CheckersAppServer
{
    public interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void GameUpdated(GameStatus gameStatus);
    }
}

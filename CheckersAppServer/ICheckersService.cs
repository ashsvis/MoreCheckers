using System;
using System.ServiceModel;

namespace CheckersAppServer
{
    [ServiceContract]
    public interface ICheckersService
    {
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



        [OperationContract]
        DateTime GetDate();
    }
}

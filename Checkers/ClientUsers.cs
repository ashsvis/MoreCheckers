using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public static partial class Client
    {
        public static Task<string[]> GetUserPropertiesAsync(string id)
        {
            var task = new Task<string[]>(() => GetUserProperties(id));
            task.Start();
            return task;
        }

        public static string[] GetUserProperties(string id)
        {
            return GetArrayMethod("GetUserProperties", () => _proxy.GetUserProperties(id), new string[] { });
        }

        public static Task<string> GetUserPasswordHashAsync(string id)
        {
            var task = new Task<string>(() => GetUserPasswordHash(id));
            task.Start();
            return task;
        }

        public static string GetUserPasswordHash(string id)
        {
            return GetMethod("GetUserPasswordHash", () => _proxy.GetUserPasswordHash(id), "");
        }

        public static bool DeleteUser(string id)
        {
            return GetMethod("DeleteUser", () => _proxy.DeleteUser(id), false);
        }

        public static bool ChangeUser(string id, string fullname, string position, string sector, string hash)
        {
            return GetMethod("ChangeUser", () => _proxy.ChangeUser(id, fullname, position, sector, hash), false);
        }

        public static bool AddUser(string fullname, string position, string sector, string hash)
        {
            return GetMethod("AddUser", () => _proxy.AddUser(fullname, position, sector, hash), false);
        }

        public static Task<string[]> GetUserNamesAsync()
        {
            var task = new Task<string[]>(GetUserNames);
            task.Start();
            return task;
        }

        public static string[] GetUserNames()
        {
            return GetMethod("GetUserNames", () => _proxy.GetUserNames(), new string[] { });
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckersAppServer
{
    public partial class CheckersService : ICheckersService
    {
        public string GetUserPasswordHash(string id)
        {
            AppData.Users.ReLoad();
            return AppData.Users.ReadString(id, "Password", "");
        }

        public string[] GetUserNames()
        {
            AppData.Users.ReLoad();
            return AppData.Users.ReadSections().Select(id =>
                string.Format("{0}\t{1}", id, AppData.Users.ReadString(id, "FullName", "")))
                .Where(fullname => !string.IsNullOrWhiteSpace(fullname)).ToArray();
        }

        public string[] GetUserProperties(string id)
        {
            var list = new List<string>();
            AppData.Users.ReLoad();
            if (AppData.Users.SectionExists(id))
            {
                list.AddRange(from prop in AppData.Users.ReadSectionKeys(id)
                              let value = AppData.Users.ReadString(id, prop, "")
                              where !string.IsNullOrWhiteSpace(value)
                              select string.Format("{0}\t{1}", prop, value));
            }
            return list.ToArray();
        }

        public bool AddUser(string fullname, string position, string sector, string hash)
        {
            try
            {
                AppData.Users.ReLoad();
                var sections = AppData.Users.ReadSections();
                if (sections.Any(section =>
                    AppData.Users.ReadString(section, "FullName", "") == fullname))
                    return false;
                var id = Guid.NewGuid().ToString();
                AppData.Users.WriteString(id, "FullName", fullname);
                AppData.Users.WriteString(id, "Password", hash);
                AppData.Users.UpdateFile();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChangeUser(string id, string fullname, string position, string sector, string hash)
        {
            try
            {
                AppData.Users.ReLoad();
                var sections = AppData.Users.ReadSections();
                if (sections.Any(section => section != id &&
                                            AppData.Users.ReadString(section, "FullName", "") == fullname))
                    return false;
                AppData.Users.WriteString(id, "FullName", fullname);
                if (!string.IsNullOrWhiteSpace(hash))
                    AppData.Users.WriteString(id, "Password", hash);
                AppData.Users.UpdateFile();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUser(string id)
        {
            try
            {
                AppData.Users.ReLoad();
                AppData.Users.EraseSection(id);
                AppData.Users.UpdateFile();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}

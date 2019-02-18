using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CheckersAppServer
{
    public static class AppData
    {
        private static string _configPath;
        private static MemIniFile _usersmif;

        public static MemIniFile Users
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_configPath))
                {
                    _configPath = Path.Combine(Environment.GetFolderPath(
                        Environment.SpecialFolder.CommonApplicationData), "Checkers");
                    if (!Directory.Exists(_configPath)) Directory.CreateDirectory(_configPath);
                }
                if (_usersmif == null)
                {
                    _usersmif = new MemIniFile(Path.Combine(_configPath, "users.ini"));
                    var id = Guid.NewGuid().ToString();
                    _usersmif.WriteString(id, "FullName", "Администратор");
                    using (var md5Hash = MD5.Create())
                    {
                        _usersmif.WriteString(id, "Password", GetMd5Hash(md5Hash, "123456"));
                    }
                    _usersmif.UpdateFile();
                }
                return _usersmif;
            }
        }

        // получить hash-строку
        public static string GetMd5Hash(HashAlgorithm md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (var i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // проверить, что hash-стока соответствует строке input
        public static bool VerifyMd5Hash(HashAlgorithm md5Hash, string input, string hash)
        {
            // Hash the input.
            var hashOfInput = GetMd5Hash(md5Hash, input);
            // Create a StringComparer an compare the hashes.
            var comparer = StringComparer.OrdinalIgnoreCase;
            return 0 == comparer.Compare(hashOfInput, hash);
        }


    }
}

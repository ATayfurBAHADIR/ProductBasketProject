using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class Hash
    {
        public static string PaswordHash(string hashString)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}", hashString);
                byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
                return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
            }

        }
    }
}

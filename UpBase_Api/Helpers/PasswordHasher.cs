using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace UpBase_Api.Helpers
{
    public class PasswordHasher
    {
        public static string HashPassword(string senha)
        {
            var salt = new byte[32];
            new RNGCryptoServiceProvider().GetBytes(salt);

            var hash = new Rfc2898DeriveBytes(senha, salt, 10000);

            return Convert.ToBase64String(hash.GetBytes(32));
        }
    }
}






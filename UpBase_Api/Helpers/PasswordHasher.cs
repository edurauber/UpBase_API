using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace UpBase_Api.Helpers
{
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            var salt = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            using (var hasher = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                hasher.Salt = salt;
                hasher.DegreeOfParallelism = 8; 
                hasher.MemorySize = 65536;

                byte[] hashBytes = hasher.GetBytes(32);
                string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                string saltAndHash = Convert.ToBase64String(salt) + ":" + hash;
                return saltAndHash;
            }
        }
    }
}

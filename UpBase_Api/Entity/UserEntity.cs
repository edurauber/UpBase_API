using System.Security.Cryptography;
using System.Xml.Linq;
using UpBase_Api.Helpers;

namespace UpBase_Api.Entity
{
        public class UserEntity
        {
            public static string DatabaseName = "User";
            public static string DatabaseValues =
                $@"Id = @ID,
                   Name = @NAME,
                   Username = @USERNAME,
                   Email = @EMAIL,
                   Password = @PASSWORD,
                   Salt = @SALT,
                   IsActive = @ISACTIVE";
            public int Id { get; set; }
            public string Name { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public bool IsActive { get; set; }
            private string HashPassword { get; set; }
            private string Salt { get; set; }
            public string Password
            {
                get
                {
                    return HashPassword;
                }
                set
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        HashPassword = PasswordHasher.HashPassword(value);
                    }
                }
            }
            public UserEntity(string name, string username, string email, string password, string salt)
            {
                Name = name;
                Username = username;
                Email = email;
                Password = password;
                Salt = salt;
                IsActive = true;
            }
            public UserEntity()
            {

            }
        public bool VerifyPassword(string password)
        {
            var hashedPassword = PasswordHasher.HashPassword(password);
            return HashPassword == hashedPassword;
        }   
    }
}


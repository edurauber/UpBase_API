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
               IsActive = @ISACTIVE";
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public UserEntity(string name, string username, string email, string password)
        {
            Name = name;
            Username = username;
            Email = email;
            Password = password;
            IsActive = true;
        }
        public UserEntity()
        {

        }
    }
}


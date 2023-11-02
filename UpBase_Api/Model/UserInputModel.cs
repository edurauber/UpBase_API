using UpBase_Api.Entity;
using UpBase_Api.Helpers;
using UpBase_Api.Model;

namespace UpBase_Api.Model
{
    public class UserInputModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsActive { get; set; }
    }
}

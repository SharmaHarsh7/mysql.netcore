using NS.Framework.Repository.Pattern.MySQL;

namespace NS.Domain.Models.Users
{
    public class User : Entity
    {
        public int ID_User { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

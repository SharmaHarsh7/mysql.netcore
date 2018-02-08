using DS.Framework.Repository.Pattern.MySQL;

namespace DS.Domain.Models.Users
{
    public class Employee : Entity
    {
        public int ID_Employee { get; set; }
        public string Name { get; set; }

        public int? ID_User { get; set; }

        public virtual User User { get; set; }
    }
}

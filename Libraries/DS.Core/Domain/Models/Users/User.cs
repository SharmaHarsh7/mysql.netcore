using DS.Core.Domain;
using System.Collections.Generic;

namespace DS.Domain.Models.Users
{
    public class User : BaseEntity
    {
        public int ID_User { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Employee> Employees{get;set;}
    }
}

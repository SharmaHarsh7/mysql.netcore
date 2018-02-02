using System.Collections.Generic;

namespace DS.Core.Domain.Models.Projects
{
    public class Project : BaseEntity
    {
        public int ID_Project { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Application> Applications{get;set;}
    }
}

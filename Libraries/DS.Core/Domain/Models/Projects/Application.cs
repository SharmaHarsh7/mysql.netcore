using DS.Core.Domain.Models.Logging;
using System.Collections.Generic;

namespace DS.Core.Domain.Models.Projects
{
    public class Application : BaseEntity
    {
        public int ID_Application { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ID_Project { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
    }
}

using DS.Core.Domain.Models.Projects;

namespace DS.Core.Domain.Models.Logging
{
    public class Log : BaseEntity
    {
        public int ID_Log { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ID_Application { get; set; }
        public virtual Application Application { get; set; }
    }
}

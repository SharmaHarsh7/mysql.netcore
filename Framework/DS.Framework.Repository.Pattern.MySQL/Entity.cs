using DS.Frameowrk.Repository.Infrastructure.Pattern;
using System.ComponentModel.DataAnnotations.Schema;

namespace DS.Framework.Repository.Pattern.MySQL
{
    public abstract class Entity : IObjectState
    {
        [NotMapped]
        public virtual ObjectState ObjectState { get; set; }
    }
}
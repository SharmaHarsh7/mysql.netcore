using NS.Frameowrk.Repository.Infrastructure.Pattern;
using System.ComponentModel.DataAnnotations.Schema;

namespace NS.Framework.Repository.Pattern.EFCore2
{
    public abstract class Entity : IObjectState
    {
        [NotMapped]
        public virtual ObjectState ObjectState { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace NS.Frameowrk.Repository.Infrastructure.Pattern
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}
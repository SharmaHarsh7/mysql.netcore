
using System.ComponentModel.DataAnnotations.Schema;

namespace DS.Frameowrk.Repository.Infrastructure.Pattern
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}
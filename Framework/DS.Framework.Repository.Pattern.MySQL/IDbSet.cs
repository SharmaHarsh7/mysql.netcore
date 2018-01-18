using DS.Framework.Repository.Pattern.MySQL;

namespace DS.Frameowrk.Repository.Pattern.MySQL
{
    internal interface IDbSet<TEntity> where TEntity : Entity, new()
    {
    }
}
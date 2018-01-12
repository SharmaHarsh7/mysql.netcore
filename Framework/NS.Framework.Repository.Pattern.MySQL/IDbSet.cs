using NS.Framework.Repository.Pattern.MySQL;

namespace NS.Frameowrk.Repository.Pattern.MySQL
{
    internal interface IDbSet<TEntity> where TEntity : Entity, new()
    {
    }
}
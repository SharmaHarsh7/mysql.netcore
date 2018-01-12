using NS.Framework.Repository.Pattern.EFCore2;

namespace NS.Frameowrk.Repository.Pattern.EFCore2
{
    internal interface IDbSet<TEntity> where TEntity : Entity, new()
    {
    }
}
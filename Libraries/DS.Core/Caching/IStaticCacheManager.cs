using DS.Core.Caching;

namespace DS.Core.Caching
{
    /// <summary>
    /// Represents a manager for caching between HTTP requests (long term caching)
    /// </summary>
    public interface IStaticCacheManager : ICacheManager
    {
    }
}
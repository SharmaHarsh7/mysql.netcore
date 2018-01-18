using System.Web;

namespace DS.Core
{
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public partial interface IWebHelper
    {
        void RestartAppDomain();
    }
}

using System.Web;

namespace DS.Core
{
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public partial interface IWebHelper
    {
        string MapPath(string path);

        void RestartAppDomain();

        string GetAppURL();
    }
}

using DS.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

using DS.Core.Infrastructure;

namespace DS.Core
{
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public partial class WebHelper : IWebHelper
    {
        #region Fields 
        #endregion

        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        public WebHelper()
        {
        }

        #endregion

        #region Utilities
        protected virtual bool TryWriteWebConfig()
        {
            try
            {
                File.SetLastWriteTimeUtc(MapPath("~/web.config"), DateTime.UtcNow);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected virtual bool TryWriteGlobalAsax()
        {
            try
            {
                File.SetLastWriteTimeUtc(MapPath("~/global.asax"), DateTime.UtcNow);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Methods
        public virtual string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath(path);
            }

            //if appliaciton is not hosted. For example, run in unit tests
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }

        public virtual void RestartAppDomain()
        {
            if (CommonHelper.GetTrustLevel() > AspNetHostingPermissionLevel.Medium)
            {
                //full trust
                HttpRuntime.UnloadAppDomain();

                TryWriteGlobalAsax();
            }
            else
            {
                //medium trust
                bool success = TryWriteWebConfig();
                if (!success)
                {
                    throw new DSException("Application needs to be restarted due to a configuration change, but was unable to do so." + Environment.NewLine +
                        "To prevent this issue in the future, a change to the web server configuration is required:" + Environment.NewLine +
                        "- run the application in a full trust environment, or" + Environment.NewLine +
                        "- give the application write access to the 'web.config' file.");
                }

                success = TryWriteGlobalAsax();
                if (!success)
                {
                    throw new DSException("Application needs to be restarted due to a configuration change, but was unable to do so." + Environment.NewLine +
                        "To prevent this issue in the future, a change to the web server configuration is required:" + Environment.NewLine +
                        "- run the application in a full trust environment, or" + Environment.NewLine +
                        "- give the application write access to the 'Global.asax' file.");
                }
            }
        }

        public virtual string GetAppURL()
        {
            string url = string.Empty;

            url = HttpContext.Current.Request.Url.AbsoluteUri;

            return url;
        }



        #endregion
    }
}

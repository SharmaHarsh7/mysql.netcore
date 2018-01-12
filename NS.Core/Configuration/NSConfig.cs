namespace NS.Core.Configuration
{
    public class NSConfig
    {
        public bool UseUnsafeLoadAssembly { get; set; }
        public bool ClearPluginShadowDirectoryOnStartup { get; set; }
        public bool IgnoreStartupTasks { get; set; }
        private string ApplicationVersion { get; set; }
    
    }
}

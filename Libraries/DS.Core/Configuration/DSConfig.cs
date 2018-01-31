namespace DS.Core.Configuration
{
    public class DSConfig
    {
        public bool UseUnsafeLoadAssembly { get; set; }
        public bool ClearPluginShadowDirectoryOnStartup { get; set; }
        public bool IgnoreStartupTasks { get; set; }
        private string ApplicationVersion { get; set; }
    
    }
}

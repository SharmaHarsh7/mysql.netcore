using DS.Core.Events;
using DS.Core.Plugins;
using DS.Domain.Models.Users;
using DS.Services.Events;
using System.IO;

namespace DS.Plugin.Test
{
    public class PluginProcessor : BasePlugin, IConsumer<EntityInserted<User>>
    {
        public PluginProcessor()
        { }

        public void HandleEvent(EntityInserted<User> eventMessage)
        {
            TextWriter textWriter = new StreamWriter("d://test.txt");
            textWriter.WriteLine(eventMessage.Entity.Name);
            textWriter.Close();

        }
    }
}

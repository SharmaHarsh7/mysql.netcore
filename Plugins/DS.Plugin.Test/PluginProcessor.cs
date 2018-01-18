using DS.Core.Events;
using DS.Core.Plugins;
using DS.Domain.Models.Users;
using DS.Services.Events;

namespace DS.Plugin.Test
{
    public class PluginProcessor : BasePlugin, IConsumer<EntityInserted<User>>
    {
        public void HandleEvent(EntityInserted<User> eventMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}

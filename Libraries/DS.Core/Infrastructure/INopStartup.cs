using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DS.Core.Infrastructure
{
    public interface INSStartup
    {
        void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration);

        void Configure(IApplicationBuilder application);

        int Order { get; }
    }
}

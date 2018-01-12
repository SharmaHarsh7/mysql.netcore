
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using NS.Core.Configuration;

namespace NS.Core.Infrastructure.DependencyManagement
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder, NSConfig config);

        int Order { get; }
    }
}

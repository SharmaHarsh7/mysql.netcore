
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using DS.Core.Configuration;

namespace DS.Core.Infrastructure.DependencyManagement
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder, NSConfig config);

        int Order { get; }
    }
}

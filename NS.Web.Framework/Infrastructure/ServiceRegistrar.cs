using Autofac;
using NS.Core.Configuration;
using NS.Core.Infrastructure;
using NS.Core.Infrastructure.DependencyManagement;
using NS.Data.Models;
using NS.Domain.Models.Users;
using NS.Frameowrk.Repository.Repositories.Pattern;
using NS.Frameowrk.Repository.UnitOfWork.Pattern;
using NS.Framework.Repository.Pattern.DataContext;
using NS.Framework.Repository.Pattern.MySQL;
using NS.Services;

namespace NS.Web.Framework.Infrastructure
{
    public class ServiceRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get { return 0; }
        }

        void IDependencyRegistrar.Register(ContainerBuilder builder, ITypeFinder typeFinder, NSConfig config)
        {
            builder.RegisterType<NSDBContext>().As<IDataContextAsync>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWorkAsync>().InstancePerLifetimeScope();
            builder.RegisterType<Repository<User>>().As<IRepositoryAsync<User>>().InstancePerLifetimeScope();
            
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }
    }
}

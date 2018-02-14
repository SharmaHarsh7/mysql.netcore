using Autofac;
using DS.Code.Domain.Models.Authentication;
using DS.Core.Configuration;
using DS.Core.Domain.Configuration;
using DS.Core.Infrastructure;
using DS.Core.Infrastructure.DependencyManagement;
using DS.Data.Models;
using DS.Data.Mongo.Entities;
using DS.Domain.Models.Users;
using DS.Frameowrk.Repository.Repositories.Pattern;
using DS.Frameowrk.Repository.UnitOfWork.Pattern;
using DS.Framework.Mongo.Repository;
using DS.Framework.Repository.Pattern.DataContext;
using DS.Framework.Repository.Pattern.MySQL;
using DS.Services;

namespace DS.Web.Framework.Infrastructure
{
    public class ServiceRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get { return 0; }
        }

        void IDependencyRegistrar.Register(ContainerBuilder builder, ITypeFinder typeFinder, DSConfig config)
        {
            builder.RegisterType<NSDBContext>().As<IDataContextAsync>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWorkAsync>().InstancePerLifetimeScope();
            builder.RegisterType<Repository<User>>().As<IRepositoryAsync<User>>().InstancePerLifetimeScope();
            builder.RegisterType<Repository<Employee>>().As<IRepositoryAsync<Employee>>().InstancePerLifetimeScope();
            builder.RegisterType<Repository<Client>>().As<IRepositoryAsync<Client>>().InstancePerLifetimeScope();
            builder.RegisterType<Repository<RefreshToken>>().As<IRepositoryAsync<RefreshToken>>().InstancePerLifetimeScope();
            builder.RegisterType<MongoRepository<Note>>().As<IMongoRepository<Note>>().InstancePerLifetimeScope();


            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();
        }
    }
}

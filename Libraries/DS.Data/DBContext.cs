using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DS.Data.Mappings.Users;
using DS.Framework.Repository.Pattern.DataContext;
using DS.Framework.Repository.Pattern.MySQL;
using DS.Data.Mappings.Configuration;
using System.Threading.Tasks;
using System.Threading;

namespace DS.Data.Models
{
    public partial class NSDBContext : DataContext, IDataContextAsync
    {

        public NSDBContext(DbContextOptions<DbContext> options)
       : base(options)
        {
            this.Database.Migrate();
        }
        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        public override int SaveChanges()
        {
            var a = this.ChangeTracker.Entries();
            this.ApplyStateChanges();
            return base.SaveChanges();
        }

        //public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess=true, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    this.ApplyStateChanges();
        //    return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new EmployeeMap());
            modelBuilder.ApplyConfiguration(new SettingMap());
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new RefreshTokenMap());
        }
    }


    public class NSDBContextFactory : IDesignTimeDbContextFactory<NSDBContext>
    {
        public NSDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseMySql("server=127.0.0.1;database=Signals;user=root;password=root");

            return new NSDBContext(optionsBuilder.Options);
        }
    }

}

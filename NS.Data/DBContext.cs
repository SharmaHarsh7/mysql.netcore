using Microsoft.EntityFrameworkCore;
using NS.Data.Mappings.Users;
using NS.Framework.Repository.Pattern.DataContext;
using NS.Framework.Repository.Pattern.MySQL;

namespace NS.Data.Models
{
    public partial class NSDBContext : DataContext, IDataContextAsync
    {

        public NSDBContext(DbContextOptions<DbContext> options)
       : base(options)
        {
            this.Database.EnsureCreated();
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            

            modelBuilder.ApplyConfiguration(new UserMap());


        }
    }
}

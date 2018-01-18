﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DS.Data.Mappings.Users;
using DS.Framework.Repository.Pattern.DataContext;
using DS.Framework.Repository.Pattern.MySQL;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new EmployeeMap());
        }
    }


    public class NSDBContextFactory : IDesignTimeDbContextFactory<NSDBContext>
    {
        public NSDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseMySQL("server=127.0.0.1;database=Signals;user=root;password=raghav");

            return new NSDBContext(optionsBuilder.Options);
        }
    }

}
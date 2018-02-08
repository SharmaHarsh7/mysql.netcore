using DS.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DS.Data.Mappings.Users
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.ID_Employee);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.HasOne(x=>x.User).WithMany(y=>y.Employees).HasForeignKey(z=>z.ID_User).IsRequired(false);
        }
    }
}

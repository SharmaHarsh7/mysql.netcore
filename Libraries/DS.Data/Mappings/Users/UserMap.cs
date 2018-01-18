using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DS.Domain.Models.Users;

namespace DS.Data.Mappings.Users
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.ID_User);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(15);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(15);
        }
    }
}

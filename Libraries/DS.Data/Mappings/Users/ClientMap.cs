using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DS.Domain.Models.Users;

namespace DS.Data.Mappings.Users
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Secret).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ApplicationType).IsRequired();
            builder.Property(x => x.Active).IsRequired();
            builder.Property(x => x.RefreshTokenLifeTime).IsRequired();
            builder.Property(x => x.AllowedOrigin).HasMaxLength(100).IsRequired();
        }
    }
}

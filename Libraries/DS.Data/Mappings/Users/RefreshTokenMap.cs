using DS.Code.Domain.Models.Authentication;
using DS.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DS.Data.Mappings.Users
{
    public class RefreshTokenMap : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Subject).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ClientId).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ProtectedTicket).IsRequired();
            builder.Property(x => x.IssuedUtc).IsRequired();
            builder.Property(x => x.ExpiresUtc).IsRequired();
        }
    }
}

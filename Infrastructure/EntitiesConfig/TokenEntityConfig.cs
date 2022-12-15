using Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfig
{
    internal class TokenEntityConfig : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> entity)
        {
            entity.ToTable("Token");

            entity.Property(e => e.Content)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.Expiration).HasColumnType("datetime");
        }
    }
}

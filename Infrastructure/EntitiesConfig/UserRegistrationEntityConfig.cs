using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfig
{
    internal class UserRegistrationEntityConfig : IEntityTypeConfiguration<UserRegistration>
    {
        public void Configure(EntityTypeBuilder<UserRegistration> entity)
        {
            entity.ToTable("UserRegistration");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.HasOne(d => d.Token)
                   .WithMany(p => p.UserRegistrations)
                   .HasForeignKey(d => d.TokenId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_UserRegistration_Token");
        }
    }
}

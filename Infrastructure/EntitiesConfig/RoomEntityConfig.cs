using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfig
{
    internal class RoomEntityConfig : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> entity)
        {
            entity.ToTable("Room");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.CreatedByNavigation)
                .WithMany(p => p.Rooms)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Room_User");

            entity.HasMany(d => d.User)
                .WithMany(p => p.RoomUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRoom",
                    l => l.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserRoom_User"),
                    r => r.HasOne<Room>().WithMany().HasForeignKey("RoomId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UserRoom_Room"),
                    j =>
                    {
                        j.HasKey("RoomId", "UserId");

                        j.ToTable("UserRoom");
                    });
        }
    }
}

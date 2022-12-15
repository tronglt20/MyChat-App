using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfig
{
    internal class MessageEntityConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> entity)
        {
            entity.ToTable("Message");

            entity.Property(e => e.Content).IsRequired();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Room)
                .WithMany(p => p.Message)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_Room");

            entity.HasOne(d => d.Sender)
                .WithMany(p => p.Messages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_User");
        }
    }
}

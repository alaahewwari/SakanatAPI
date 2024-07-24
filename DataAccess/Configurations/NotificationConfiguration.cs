using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder
            .HasOne(n => n.Sender)
            .WithMany(u => u.SentNotifications)
            .HasForeignKey(n => n.SenderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
        .HasMany(r=>r.Receivers)
        .WithMany(n=>n.Notifications)
        .UsingEntity<Dictionary<string, object>>(
        "Receivers",
            j => j.HasOne<ApplicationUser>().WithMany().HasForeignKey("ReceiverId").OnDelete(DeleteBehavior.Cascade),
            j => j.HasOne<Notification>().WithMany().HasForeignKey("NotificationId").OnDelete(DeleteBehavior.Cascade),
            j => j.HasKey("ReceiverId", "NotificationId"));
    }
}
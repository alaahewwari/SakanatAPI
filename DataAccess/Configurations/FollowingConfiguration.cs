using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DataAccess.Configurations
{
    public class FollowingConfiguration : IEntityTypeConfiguration<UserFollows>
    {
        public void Configure(EntityTypeBuilder<UserFollows> builder)
        {
            builder
                .HasKey(f => new { f.FollowerId, f.FollowingId });

            builder
                            .HasOne(f => f.Follower)
                            .WithMany(u => u.FollowingUsers)
                            .HasForeignKey(f => f.FollowerId)
                            .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(f => f.Following)
                .WithMany(u => u.FollowerUsers)
                .HasForeignKey(f => f.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

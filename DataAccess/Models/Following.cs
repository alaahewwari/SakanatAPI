namespace DataAccess.Models
{
    public class UserFollows
    {
        public Guid FollowingId { get; set; }
        public ApplicationUser Following { get; set; }
        public Guid FollowerId { get; set; }
        public ApplicationUser Follower { get; set; } 
    }
}

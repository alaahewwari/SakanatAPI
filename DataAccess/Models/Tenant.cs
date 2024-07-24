

namespace DataAccess.Models
{
    public class Tenant : ApplicationUser
    {
        public string Note { get; set; }
        public Guid OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        public ICollection<Contract> Contracts { get; set; } = [];
    }
}

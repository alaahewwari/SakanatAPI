using Microsoft.AspNetCore.Identity;
namespace DataAccess.Models;
public class Role : IdentityRole<Guid>
{
    public ICollection<ApplicationUser> Users { get; set; }=new List<ApplicationUser>();
}
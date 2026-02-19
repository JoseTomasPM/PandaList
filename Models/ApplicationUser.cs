using Microsoft.AspNetCore.Identity;

namespace PandaList.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}

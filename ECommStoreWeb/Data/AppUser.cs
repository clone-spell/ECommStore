using Microsoft.AspNetCore.Identity;

namespace ECommStoreWeb.Data
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public string? PhotoLink { get; set; }
    }
}
using Microsoft.AspNetCore.Identity;

namespace SiteYonetimSistemi.API.Models
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole() : base() { }
        public AppRole(string roleName) : base(roleName) { }
    }
}

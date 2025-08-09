using Microsoft.AspNetCore.Identity;

namespace DepresStore.Modules.Identity.Domain.Entities
{
    public class Role : IdentityRole
    {
        public Role() : base() { }

        public Role(string roleName) : base(roleName) { }
    }
}
using Microsoft.AspNetCore.Identity;

namespace DepresStore.Modules.Identity.Domain.Entities
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; }

        public string? LastName { get; set; }
    }
}
using DepresStore.Modules.Identity.Domain.Entities;
using DepresStore.Modules.Identity.Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DepresStore.Modules.Identity.Infrastructure.Data
{
    public class AppIdentityDbContext : IdentityDbContext<User, Role, string>
    {
        public const string Schema = "Identity";

        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            base.OnModelCreating(modelBuilder);

            new UserConfiguration().Configure(modelBuilder.Entity<User>());
        }
    }
}
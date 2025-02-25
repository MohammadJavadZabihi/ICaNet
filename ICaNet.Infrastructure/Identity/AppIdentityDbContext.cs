using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ICaNet.Infrastructure.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("User");
            builder.Entity<IdentityRole<string>>().ToTable("Role");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");

            //builder.Ignore<IdentityUserClaim<string>>();
            //builder.Ignore<IdentityUserLogin<string>>();
            //builder.Ignore<IdentityRoleClaim<string>>();
            //builder.Ignore<IdentityUserToken<string>>();
        }
    }
}

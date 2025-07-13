using EventExplorer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventExplorerWebApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Location { get; set; }
     //   public DbSet<User> User { get; set; }

    }
}

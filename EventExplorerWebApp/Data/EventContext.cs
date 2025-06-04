using EventExplorer;
using Microsoft.EntityFrameworkCore;

namespace EventExplorerWebApp.Data
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> options) : base(options)
        {
        }

        // DbSets representing the tables in the database
        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Location { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Event entity
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.EventID);
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.EventCategory).HasMaxLength(50);
                entity.Property(e => e.StartTime).HasColumnType("datetime");
                entity.Property(e => e.ImageURL).HasMaxLength(500);
                entity.Property(e => e.EventURL).HasMaxLength(500);
                entity.Property(e => e.OriginalID).HasMaxLength(100);

                // Configure the relationship with Location
                entity.HasOne(e => e.EventLocation).WithMany().HasForeignKey("LocationId").OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Location entity
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(l => l.LocationId);
                entity.Property(l => l.VenueName).HasMaxLength(200);
                entity.Property(l => l.AddressLine1).HasMaxLength(200);
                entity.Property(l => l.AddressLine2).HasMaxLength(200);
                entity.Property(l => l.City).HasMaxLength(100);
                entity.Property(l => l.PostalCode).HasMaxLength(20);
            });
        }
    }
}

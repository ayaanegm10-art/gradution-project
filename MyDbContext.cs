using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using try4.Models;

namespace try4.data
{
    public class MyDbContext : IdentityDbContext<User, Microsoft.AspNetCore.Identity.IdentityRole<int>, int>
    {
        public DbSet<Trip> Trips { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<Center> Centers { get; set; } = null!;
        public DbSet<Rout> Routs { get; set; } = null!;
        public DbSet<Search> Searches { get; set; } = null!;
        public MyDbContext() { }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // empty; configuration is done in Program.cs via DI
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>(b =>
            { 
            b.Property(b => b.BookingId)
            .UseIdentityColumn(seed: 1, increment: 1);
             b.Property(b => b.BookingTime)
                .HasDefaultValueSql("CAST(GETDATE() AS TIME)");
                //.ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Center>(b =>
            {
                b.HasKey(c => c.CenterId);
                b.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Rout>(b =>
            {
              
                b.HasKey(r => r.RouteId);

                b.Property(r => r.CenterAid).HasColumnName("CenterAId");
                b.Property(r => r.CenterBid).HasColumnName("CenterBId");

                b.HasOne(r => r.CenterA)
                 .WithMany(c => c.RouteCenterAs)
                 .HasForeignKey(r => r.CenterAid)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(r => r.CenterB)
                 .WithMany(c => c.RouteCenterBs)
                 .HasForeignKey(r => r.CenterBid)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Trip>(b =>
            {
                b.HasKey(t => t.TripId);

                b.HasOne(t => t.Rout)
                 .WithMany(r => r.Trips)
                 .HasForeignKey(t => t.RouteId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(t => t.FromCenter)
                 .WithMany(c => c.TripFromCenters)
                 .HasForeignKey(t => t.FromCenterId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(t => t.ToCenter)
                 .WithMany(c => c.TripToCenters)
                 .HasForeignKey(t => t.ToCenterId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Search>(b =>
            {
                b.HasKey(s => s.SearchId);

                b.HasOne(s => s.User)
                 .WithMany(u => u.Searches)
                 .HasForeignKey(s => s.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(s => s.FromCenter)
                 .WithMany()
                 .HasForeignKey(s => s.FromId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(s => s.ToCenter)
                 .WithMany()
                 .HasForeignKey(s => s.ToId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }

}

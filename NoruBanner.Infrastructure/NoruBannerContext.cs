using Microsoft.EntityFrameworkCore;
using NoruBanner.Infrastructure.Entities;

namespace NoruBanner.Infrastructure
{
    public class NoruBannerContext : DbContext
    {
        public NoruBannerContext(DbContextOptions<NoruBannerContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UserAction> UserActions { get; set; }
        public DbSet<ServerUser> ServerUsers { get; set; }
        public DbSet<Banner> Banners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}

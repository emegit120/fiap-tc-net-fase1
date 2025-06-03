using FIAPTechChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FIAPTechChallenge.Infrastructure.Data
{
    public class FiapDbContext(DbContextOptions<FiapDbContext> options) : DbContext(options)
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Promotion> Promotions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Games)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Promotion>()
                .HasMany(p => p.Games)
                .WithMany();

            modelBuilder.Entity<Game>()
                .HasMany(g => g.Categories)
                .WithMany(c => c.Games);


            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "User" },
                new Role { Id = 2, Name = "Admin" }
            );


        }

    }
}

using Microsoft.EntityFrameworkCore;
using Hejl_RecipeWebApplication.Entities;

namespace Hejl_RecipeWebApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Reviews)
                .WithOne(rv => rv.Recipe)
                .HasForeignKey(rv => rv.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(rv => rv.Recipe)
                .WithMany(r => r.Reviews)
                .HasForeignKey(rv => rv.RecipeId);

        }
    }
}

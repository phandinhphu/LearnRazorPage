using Microsoft.EntityFrameworkCore;
using WebAppTest.Models;

namespace WebAppTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply global filter for soft delete
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);

            // Fluent API
            #region Product
            modelBuilder.Entity<Product>()
                        .HasOne(p => p.Category)
                        .WithMany(c => c.Products)
                        .HasForeignKey(p => p.CategoryId)
                        .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Category
            modelBuilder.Entity<Category>()
                        .HasIndex(c => c.Name)
                        .IsUnique();
            #endregion

            #region User
            modelBuilder.Entity<User>()
                        .HasIndex(u => new { u.Email, u.DisplayName })
                        .IsUnique();

            modelBuilder.Entity<User>()
                        .Property(u => u.Role)
                        .HasConversion<string>()
                        .HasDefaultValue(UserRole.Customer);
            #endregion
        }
    }
}

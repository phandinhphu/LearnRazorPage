using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using WebAppTest.Models;

namespace WebAppTest.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
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

            // Loai bỏ "AspNet" prefix trong ten bang do Identity tao ra
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName != null && tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

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
            //modelBuilder.Entity<User>()
            //            .HasIndex(u => new { u.Email, u.DisplayName })
            //            .IsUnique();

            //modelBuilder.Entity<User>()
            //            .Property(u => u.Role)
            //            .HasConversion<string>()
            //            .HasDefaultValue(UserRole.Customer);
            #endregion
        }
    }
}

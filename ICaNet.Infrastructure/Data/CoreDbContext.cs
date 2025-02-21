using ICaNet.ApplicationCore.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICaNet.Infrastructure.Data
{
    public class CoreDbContext : DbContext
    {
        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Product Realtions

            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new {pc.ProductId, pc.CategoryId});

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.SuppLier)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.SuppLierdId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.UnitOfMeasurement)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.UnitOfMeasurementId);

            #endregion
        }
    }
}

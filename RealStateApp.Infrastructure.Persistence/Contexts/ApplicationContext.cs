using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }


        public DbSet<ClientFavoriteProperty> ClientFavoriteProperties { get; set; }
        public DbSet<Improvement> Improvements { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImprovement> PropertyImprovements { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<PropertyImage> PropertiesImages { get; set; }
        public DbSet<SaleCategory> SaleCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Tables
            modelBuilder.Entity<ClientFavoriteProperty>().ToTable("ClientFavoriteProperties");
            modelBuilder.Entity<Improvement>().ToTable("Improvements");
            modelBuilder.Entity<Property>().ToTable("Properties");
            modelBuilder.Entity<PropertyType>().ToTable("PropertyTypes");
            modelBuilder.Entity<PropertyImage>().ToTable("PropertyImages");
            modelBuilder.Entity<SaleCategory>().ToTable("SaleCategories");
            modelBuilder.Entity<PropertyImprovement>().ToTable("PropertyImprovements");
            #endregion

            #region Primary Keys
            modelBuilder.Entity<ClientFavoriteProperty>().HasKey(k => k.Id);
            modelBuilder.Entity<Improvement>().HasKey(k => k.Id);
            modelBuilder.Entity<Property>().HasKey(k => k.Id);
            modelBuilder.Entity<PropertyType>().HasKey(k => k.Id);
            modelBuilder.Entity<PropertyImage>().HasKey(k => k.Id);
            modelBuilder.Entity<PropertyImprovement>().HasKey(k => k.Id);
            modelBuilder.Entity<SaleCategory>().HasKey(k => k.Id);
            #endregion

            #region Relations
            modelBuilder.Entity<Property>()
                .HasMany(k => k.Improvements)
                .WithOne(k => k.Property)
                .HasForeignKey(k=>k.PropertyId);

            modelBuilder.Entity<Property>()
                .HasOne(k => k.PropertyType)
                .WithMany(k => k.Properties)
                .HasForeignKey(k => k.PropertyTypeId);

            modelBuilder.Entity<Property>()
                .HasMany(k => k.Images)
                .WithOne(k => k.Property)
                .HasForeignKey(k => k.PropertyId);

            modelBuilder.Entity<Property>()
                .HasOne(k => k.SaleCategory)
                .WithMany(k => k.Properties)
                .HasForeignKey(k => k.SaleCategoryId);

            modelBuilder.Entity<Improvement>()
                .HasMany(k => k.Properties)
                .WithOne(k => k.Improvement)
                .HasForeignKey(k => k.ImprovementId);

            modelBuilder.Entity<SaleCategory>()
                .HasMany(p => p.Properties)
                .WithOne(p => p.SaleCategory)
                .HasForeignKey(k => k.SaleCategoryId);
            #endregion
        }
    }
}

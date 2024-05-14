using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using MedEquipCentral.Models;

    public class MedEquipCentralDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<JobFunction> JobFunctions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SimilarItem> SimilarItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        modelBuilder.Entity<SimilarItem>()
           .HasKey(si => new { si.Id }); 

        modelBuilder.Entity<SimilarItem>()
            .HasOne(si => si.Product)
            .WithMany(p => p.SimilarItems)
            .HasForeignKey(si => si.ProductId)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<SimilarItem>()
            .HasOne(si => si.SimilarProduct)
            .WithMany()
            .HasForeignKey(si => si.SimilarProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
        .HasMany(p => p.SimilarItems)
        .WithOne(si => si.Product)
        .OnDelete(DeleteBehavior.Cascade);
    }

    public MedEquipCentralDbContext(DbContextOptions<MedEquipCentralDbContext> context) : base(context)
    { 
    } 
        };

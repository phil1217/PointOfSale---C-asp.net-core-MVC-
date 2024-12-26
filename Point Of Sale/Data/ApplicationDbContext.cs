using Microsoft.EntityFrameworkCore;
using Point_Of_Sale.Models;

namespace Point_Of_Sale.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employees>()
                .HasOne(e => e.Manager) 
                .WithMany() 
                .HasForeignKey(e => e.ManagerID)  
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Order)
                .WithMany()
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Products>()
               .HasOne(p => p.Category)
               .WithMany()
               .HasForeignKey(p => p.CategoryID)
               .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<ProductSupplier>()
               .HasOne(ps => ps.Supplier)
               .WithMany()
               .HasForeignKey(ps => ps.SupplierID)
               .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ProductSupplier>()
               .HasOne(ps => ps.Product)
               .WithMany()
               .HasForeignKey(ps => ps.ProductID)
               .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Reports>()
               .HasOne(r => r.GeneratedBy)
               .WithMany()
               .HasForeignKey(r => r.EmployeeID)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Managers>()
                .HasOne(m => m.Employees)
                .WithOne(e => e.Manager)
                .HasForeignKey<Managers>(m => m.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);

        }

        public DbSet<Customers> Customers { get; set; }

        public DbSet<DiscountRules> DiscountRules { get; set; }

        public DbSet<Employees> Employees { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<ProductCategories> ProductCategories { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<ProductSupplier> ProductSupplier { get; set; }

        public DbSet<Reports> Reports { get; set; }

        public DbSet<Suppliers> Suppliers { get; set; }

        public DbSet<TaxRules> TaxRules { get; set; }

        public DbSet<Managers> Managers { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using EcomSales.Models;

namespace EcomSales
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .ToTable("Customer").HasKey(c => c.Id);

            modelBuilder.Entity<Order>()
                .ToTable("Order").HasKey(c => c.Id);

            modelBuilder.Entity<OrderItem>()
                .ToTable("OrderItem").HasKey(c => c.Id);

            modelBuilder.Entity<Product>()
                .ToTable("Product").HasKey(c => c.Id);
        }
    }
}

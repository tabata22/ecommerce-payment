using System.Reflection;
using Ecommerce.Payment.Domain.CardAggregate;
using Ecommerce.Payment.Domain.OrderAggregate;
using Ecommerce.Payment.Domain.ProductAggregate;
using Ecommerce.Payment.Domain.TransactionAggregate;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Payment.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Card> Cards { get; set; }
    
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderItem> OrderItems { get; set; }
    
    public DbSet<Product> Products { get; set; }
    
    public DbSet<Transaction> Transactions { get; set; }
}
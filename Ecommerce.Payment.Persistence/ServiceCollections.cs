using Ecommerce.Payment.Application.Cards.Queries;
using Ecommerce.Payment.Application.Orders.Queries;
using Ecommerce.Payment.Application.Transactions.Queries;
using Ecommerce.Payment.Domain.CardAggregate;
using Ecommerce.Payment.Domain.OrderAggregate;
using Ecommerce.Payment.Domain.ProductAggregate;
using Ecommerce.Payment.Domain.TransactionAggregate;
using Ecommerce.Payment.Persistence.Cards;
using Ecommerce.Payment.Persistence.Orders;
using Ecommerce.Payment.Persistence.Products;
using Ecommerce.Payment.Persistence.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Payment.Persistence;

public static class ServiceCollections
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options 
            => options.UseNpgsql(configuration.GetConnectionString("MasterConnection"))
                .UseSnakeCaseNamingConvention());
        
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        
        services.AddScoped<ICardQueries, CardQueries>();
        services.AddScoped<ITransactionQueries, TransactionQueries>();
        services.AddScoped<IOrderQueries, OrderQueries>();
        
        return services;
    }
}
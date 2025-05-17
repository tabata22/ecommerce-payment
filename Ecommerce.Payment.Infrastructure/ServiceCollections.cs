using System.Data.Common;
using Ecommerce.Payment.Application.CatalogService;
using Ecommerce.Payment.Application.Identity;
using Ecommerce.Payment.Application.PaymentProviders;
using Ecommerce.Payment.Infrastructure.Catalog;
using Ecommerce.Payment.Infrastructure.Identity;
using Ecommerce.Payment.Infrastructure.PaymentProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Payment.Infrastructure;

public static class ServiceCollections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("CatalogService", (_, client) =>
        { 
            client.BaseAddress = new Uri(configuration["CatalogServiceUrl"]);
        });
        services.AddScoped<ICatalogService, CatalogService>();
        
        var paymentSettings = new BogPaymentSettings();
        configuration.Bind(BogPaymentSettings.SettingsKey, paymentSettings);
        
        services.AddSingleton(paymentSettings);

        services.AddHttpClient("BogPayment", (_, httpClient) =>
        {
            httpClient.BaseAddress = new Uri(paymentSettings.BasePath);
        });
        
        services.AddSingleton<IPaymentClient, PaymentClient>();
        services.AddMemoryCache();
        
        services.AddScoped<IUserService, UserService>();
        
        return services;
    }
}
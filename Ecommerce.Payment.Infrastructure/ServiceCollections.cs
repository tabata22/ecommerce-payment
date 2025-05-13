using Ecommerce.Payment.Application.Identity;
using Ecommerce.Payment.Application.PaymentProviders;
using Ecommerce.Payment.Infrastructure.Identity;
using Ecommerce.Payment.Infrastructure.PaymentProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ecommerce.Payment.Infrastructure;

public static class ServiceCollections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
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
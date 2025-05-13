namespace Ecommerce.Payment.Infrastructure.Messaging.Products.Events;

public record ProductCreatedEvent(long Id, string Name, decimal Price, string Image);
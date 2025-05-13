using Ecommerce.Payment.Api.Endpoints;
using Ecommerce.Payment.Application;
using Ecommerce.Payment.Infrastructure;
using Ecommerce.Payment.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await MigrateAsync();
}

app.MapEndpoints();
app.UseHttpsRedirection();

app.Run();

return;

async Task MigrateAsync()
{
    await using var scope = app.Services.CreateAsyncScope();
    await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
  
    await dbContext.Database.MigrateAsync();
}
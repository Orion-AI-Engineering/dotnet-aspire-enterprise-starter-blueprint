using Orion.Starter.Api.Contracts;
using Orion.Starter.Api.Features;
using Orion.Starter.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddSingleton<IOrderStore, InMemoryOrderStore>();

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapGet("/orders/{id:guid}", (Guid id, IOrderStore store) =>
{
    var order = store.Get(id);
    return order is null ? Results.NotFound() : Results.Ok(order);
});

app.MapPost("/orders", (CreateOrderRequest request, IOrderStore store) =>
{
    var order = store.Create(request.CustomerName, request.ProductCode, request.Quantity);
    // In a production template this is where the message would be published.
    return Results.Accepted($"/orders/{order.Id}", order);
});

app.MapDefaultEndpoints();

app.Run();

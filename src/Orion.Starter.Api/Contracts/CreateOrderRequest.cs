namespace Orion.Starter.Api.Contracts;

public sealed record CreateOrderRequest(string CustomerName, string ProductCode, int Quantity);

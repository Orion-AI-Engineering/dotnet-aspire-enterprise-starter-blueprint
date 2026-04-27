namespace Orion.Starter.Api.Messaging;

public sealed record OrderRequested(Guid OrderId, string CustomerName, string ProductCode, int Quantity);

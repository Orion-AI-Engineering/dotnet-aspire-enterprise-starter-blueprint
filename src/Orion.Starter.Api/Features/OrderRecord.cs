namespace Orion.Starter.Api.Features;

public sealed record OrderRecord(Guid Id, string CustomerName, string ProductCode, int Quantity, string Status);

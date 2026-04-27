namespace Orion.Starter.Api.Configuration;

public sealed class OrderOptions
{
    public const string SectionName = "Orders";
    public int DefaultQuantity { get; init; } = 1;
}

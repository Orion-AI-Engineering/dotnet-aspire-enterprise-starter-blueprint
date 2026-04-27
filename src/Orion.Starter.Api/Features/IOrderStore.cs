namespace Orion.Starter.Api.Features;

public interface IOrderStore
{
    OrderRecord Create(string customerName, string productCode, int quantity);
    OrderRecord? Get(Guid id);
}

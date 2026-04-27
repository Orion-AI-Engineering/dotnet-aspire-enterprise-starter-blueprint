using System.Collections.Concurrent;

namespace Orion.Starter.Api.Features;

public sealed class InMemoryOrderStore : IOrderStore
{
    private readonly ConcurrentDictionary<Guid, OrderRecord> _orders = new();

    public OrderRecord Create(string customerName, string productCode, int quantity)
    {
        var order = new OrderRecord(Guid.NewGuid(), customerName, productCode, quantity, "Queued");
        _orders[order.Id] = order;
        return order;
    }

    public OrderRecord? Get(Guid id) => _orders.TryGetValue(id, out var order) ? order : null;
}

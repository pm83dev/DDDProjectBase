using System;

namespace DDDTestProject
{
   public class OrderCommandHandler
{
    private readonly IEventStore _eventStore;

    public OrderCommandHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task HandleCreateOrderAsync(Guid orderId, string customerId)
    {
        var order = Order.CreateOrder(orderId, customerId);
        await _eventStore.SaveEventsAsync(orderId, order.Events);
    }

    public async Task HandleShipOrderAsync(Guid orderId)
    {
        var events = await _eventStore.GetEventsAsync(orderId);
        var order = RebuildOrder(events);
        order.ShipOrder();
        await _eventStore.SaveEventsAsync(orderId, order.Events);
    }

    private Order RebuildOrder(IEnumerable<IEvent> events)
    {
        var order = new Order();
        foreach (var @event in events)
        {
            order.Apply(@event);
        }
        return order;
    }
}

public class OrderQueryHandler
{
    private readonly IEventStore _eventStore;

    public OrderQueryHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<Order> GetOrderAsync(Guid orderId)
    {
        var events = await _eventStore.GetEventsAsync(orderId);
        var order = new Order();
        foreach (var @event in events)
        {
            order.Apply(@event);
        }
        return order;
    }
}

}

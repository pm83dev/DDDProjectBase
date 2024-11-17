using System;

namespace DDDTestProject
{
    public class Order
{
    public Guid OrderId { get; private set; }
    public string CustomerId { get; private set; }
    public bool Shipped { get; private set; }

    public List<IEvent> Events { get; } = new List<IEvent>();

    public static Order CreateOrder(Guid orderId, string customerId)
    {
        var order = new Order();
        order.Apply(new OrderCreated(orderId, customerId, DateTime.UtcNow));
        return order;
    }

    public void ShipOrder()
    {
        if (Shipped) throw new InvalidOperationException("Order already shipped.");
        Apply(new OrderShipped(OrderId, DateTime.UtcNow));
    }

    public void Apply(IEvent @event)
    {
        Events.Add(@event);
        // Apply the event to change internal state.
        if (@event is OrderCreated oc)
        {
            OrderId = oc.OrderId;
            CustomerId = oc.CustomerId;
        }
        else if (@event is OrderShipped)
        {
            Shipped = true;
        }
    }
}

}

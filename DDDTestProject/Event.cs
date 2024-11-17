using System;

namespace DDDTestProject
{
    public interface IEvent { }

public class OrderCreated : IEvent
{
    public Guid OrderId { get; }
    public string CustomerId { get; }
    public DateTime CreatedAt { get; }

    public OrderCreated(Guid orderId, string customerId, DateTime createdAt)
    {
        OrderId = orderId;
        CustomerId = customerId;
        CreatedAt = createdAt;
    }
}

public class OrderShipped : IEvent
{
    public Guid OrderId { get; }
    public DateTime ShippedAt { get; }

    public OrderShipped(Guid orderId, DateTime shippedAt)
    {
        OrderId = orderId;
        ShippedAt = shippedAt;
    }
}

}

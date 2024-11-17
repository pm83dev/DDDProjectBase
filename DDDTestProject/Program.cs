namespace DDDTestProject;
class Program
{
    static async Task Main(string[] args)
    {
        var eventStore = new InMemoryEventStore();

        // Crea un comando handler per gestire i comandi
        var commandHandler = new OrderCommandHandler(eventStore);
        // Crea un query handler per leggere gli ordini
        var queryHandler = new OrderQueryHandler(eventStore);

        // Simula la creazione di un ordine
        var orderId = Guid.NewGuid();
        var customerId = "customer-123";
        await commandHandler.HandleCreateOrderAsync(orderId, customerId);
        Console.WriteLine($"Order {orderId} created for customer {customerId}");

        // Simula la spedizione dell'ordine
        await commandHandler.HandleShipOrderAsync(orderId);
        Console.WriteLine($"Order {orderId} has been shipped.");

        // Recupera l'ordine e visualizza lo stato
        var order = await queryHandler.GetOrderAsync(orderId);
        Console.WriteLine($"Order {orderId} is shipped: {order.Shipped}");
    }
}

using System;

namespace DDDTestProject
{
   public interface IEventStore
{
    Task SaveEventsAsync(Guid aggregateId, IEnumerable<IEvent> events);
    Task<IEnumerable<IEvent>> GetEventsAsync(Guid aggregateId);
}

public class InMemoryEventStore : IEventStore
{
    private readonly Dictionary<Guid, List<IEvent>> _store = new();

    public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<IEvent> events)
    {
        if (!_store.ContainsKey(aggregateId))
        {
            _store[aggregateId] = new List<IEvent>();
        }
        _store[aggregateId].AddRange(events);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<IEvent>> GetEventsAsync(Guid aggregateId)
    {
        return await Task.FromResult(_store.ContainsKey(aggregateId) ? _store[aggregateId] : new List<IEvent>());
    }
}

}

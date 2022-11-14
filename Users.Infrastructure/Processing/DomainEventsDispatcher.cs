using Domain.Shared.Business.Aggregates;
using MassTransit;
using Users.Database;

namespace Users.Infrastructure.Processing;

public class DomainEventsDispatcher : IDomainEventsDispatcher
{
    private readonly UsersContext _usersContext;
    private IBus _bus;
    
    public DomainEventsDispatcher(
        UsersContext usersContext,
        IBus bus
        )
    {
        _usersContext = usersContext;
        _bus = bus;
    }
    public async Task DispatchEventsAsync(CancellationToken token = default)
    {
        try
        {
            var entitiesWithChange = this._usersContext.ChangeTracker
                .Entries<IAggregate>()
                .Where(s => s.Entity.DomainEvents.Any());
            
            var domainEvents = entitiesWithChange
                .SelectMany(s => s.Entity.DomainEvents)
                .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await _bus.Publish(domainEvent.GenerateSipmlifiedEvent(), token).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        
    }
}
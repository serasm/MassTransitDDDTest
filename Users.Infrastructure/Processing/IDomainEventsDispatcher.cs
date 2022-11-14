using Domain.Shared.Events.DomainEvents;

namespace Users.Infrastructure.Processing;

public interface IDomainEventsDispatcher
{
    Task DispatchEventsAsync(CancellationToken token = default);
}
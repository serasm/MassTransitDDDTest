using Domain.Shared.Business.Aggregates;
using Domain.Shared.Business.UniqueIdValueObjects;

namespace Domain.Shared.Database;

public interface IUnitOfWork
{
    IReadWriteRepository<TAggregate, TUniqueId> GetRepository<TAggregate, TUniqueId>()
        where TAggregate : Aggregate<TUniqueId>
        where TUniqueId : IUniqueId;
    Task<int> CommitAsync(CancellationToken token = default);
}
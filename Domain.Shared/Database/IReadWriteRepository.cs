using Domain.Shared.Business.Aggregates;
using Domain.Shared.Business.UniqueIdValueObjects;

namespace Domain.Shared.Database;

public interface IReadWriteRepository<TAggregate, TUniqueId> : IReadRepository<TAggregate>, IWriteRepository<TAggregate> 
    where TAggregate : Aggregate<TUniqueId>
    where TUniqueId : IUniqueId
{
    
}
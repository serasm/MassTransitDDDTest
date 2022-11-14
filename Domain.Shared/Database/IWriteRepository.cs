using Domain.Shared.Business.Aggregates;

namespace Domain.Shared.Database;

public interface IWriteRepository<TAggregate> 
    where TAggregate : class, IAggregate
{
    Task AddAsync(TAggregate aggregate);
    Task AddAsync(IEnumerable<TAggregate> aggregates);
    Task UpdateAsync(TAggregate aggregate);
    Task UpdateAsync(IEnumerable<TAggregate> aggregates);
    Task DeleteAsync(TAggregate aggregate);
    Task DeleteAsync(IEnumerable<TAggregate> aggregates);
}
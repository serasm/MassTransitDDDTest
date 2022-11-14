using System.Linq.Expressions;
using Domain.Shared.Business.Aggregates;
using Domain.Shared.Business.UniqueIdValueObjects;

namespace Domain.Shared.Database;

public interface IReadRepository<TAggregate> 
    where TAggregate : IAggregate
{
    Task<IQueryable<TAggregate>> AllAsync();
    Task<TAggregate> FindAsync(Expression<Func<TAggregate, bool>> expression);
    Task<TAggregate> FindByAsync(int id);
    Task<IQueryable<TAggregate>> FilterByAsync(Expression<Func<TAggregate, bool>> expression);
}
using System.Linq.Expressions;
using Domain.Shared.Business.Aggregates;
using Domain.Shared.Business.UniqueIdValueObjects;
using Domain.Shared.Database;
using Microsoft.EntityFrameworkCore;

namespace Users.Infrastructure.Database;

public class Repository<TAggregate, TUniqueId> : IReadWriteRepository<TAggregate, TUniqueId>
    where TAggregate : Aggregate<TUniqueId>
    where TUniqueId : IUniqueId
{
    private readonly DbSet<TAggregate> _dbSet;

    public Repository(DbSet<TAggregate> dbSet)
    {
        _dbSet = dbSet ?? throw new ArgumentNullException(nameof(_dbSet));
    }

    #region ReadRepository
    public Task<IQueryable<TAggregate>> AllAsync() => Task.FromResult(_dbSet.AsQueryable());

    public async Task<TAggregate> FindAsync(Expression<Func<TAggregate, bool>> expression) => 
        (await FilterByAsync(expression)).SingleOrDefault();

    public Task<TAggregate> FindByAsync(int id) =>
        _dbSet.FirstOrDefaultAsync(aggregate => aggregate.Id == id);

    public async Task<IQueryable<TAggregate>> FilterByAsync(Expression<Func<TAggregate, bool>> expression) =>
        (await AllAsync()).Where(expression).AsQueryable();
    #endregion

    #region WriteRepository

    public async Task AddAsync(TAggregate aggregate)
    { 
        await _dbSet.AddAsync(aggregate);
    }

    public async Task AddAsync(IEnumerable<TAggregate> aggregates) => _dbSet.AddRangeAsync(aggregates);

    public Task UpdateAsync(TAggregate aggregate) => Task.FromResult(_dbSet.Update(aggregate));

    public Task UpdateAsync(IEnumerable<TAggregate> aggregates)
    {
        _dbSet.UpdateRange(aggregates);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TAggregate aggregate) => Task.FromResult(_dbSet.Remove(aggregate));

    public Task DeleteAsync(IEnumerable<TAggregate> aggregates)
    {
        _dbSet.RemoveRange(aggregates);
        return Task.CompletedTask;
    }
    #endregion
}
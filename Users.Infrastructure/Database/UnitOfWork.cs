using Domain.Shared.Business.Aggregates;
using Domain.Shared.Business.UniqueIdValueObjects;
using Domain.Shared.Database;
using Users.Database;
using Users.Infrastructure.Database;
using Users.Infrastructure.Processing;

namespace Users.Infrastructure.Domain;

public class UnitOfWork : IUnitOfWork
{
    private readonly UsersContext _context;
    private readonly IDomainEventsDispatcher _dispatcher;
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(
        UsersContext context,
        IDomainEventsDispatcher dispatcher)
    {
        _context = context;
        
        _dispatcher = dispatcher;
        
        _repositories = new Dictionary<Type, object>();
    }
    
    public IReadWriteRepository<TAggregate, TUniqueId> GetRepository<TAggregate, TUniqueId>() where TAggregate : Aggregate<TUniqueId> where TUniqueId : IUniqueId
    {
        if (_repositories.ContainsKey(typeof(TAggregate)))
        {
            return _repositories[typeof(TAggregate)] as IReadWriteRepository<TAggregate, TUniqueId>;
        }

        Repository<TAggregate, TUniqueId>
            repository = new Repository<TAggregate, TUniqueId>(_context.Set<TAggregate>());
        _repositories.Add(typeof(TAggregate), repository);

        return repository;
    }

    public async Task<int> CommitAsync(CancellationToken token = default)
    {
        var result = await _context.SaveChangesAsync(token);
        await _dispatcher.DispatchEventsAsync(token);
        return result;
    }
}
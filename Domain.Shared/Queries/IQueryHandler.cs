using MassTransit;

namespace Domain.Shared.Queries;

public interface IQueryHandler<TQuery, TQueryResult> : IConsumer<TQuery> 
    where TQuery : class, IQuery<TQueryResult>
    where TQueryResult : class
{
    Task<TQueryResult> HandleQueryAsync(ConsumeContext<TQuery> context);
}
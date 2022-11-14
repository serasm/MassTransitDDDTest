using MassTransit;

namespace Domain.Shared.Queries;

public abstract class QueryHandlerBase<TQuery, TQueryResult> : IQueryHandler<TQuery, TQueryResult> 
    where TQuery : class, IQuery<TQueryResult>
    where TQueryResult : class
{
    public async Task Consume(ConsumeContext<TQuery> context)
    {
        var result = await HandleQueryAsync(context);

        await context.RespondAsync<TQueryResult>(result);
    }

    public abstract Task<TQueryResult> HandleQueryAsync(ConsumeContext<TQuery> context);
}
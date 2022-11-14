using MassTransit;

namespace Domain.Shared.Commands;

public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand> 
    where TCommand : class, ICommand
{
    public async Task Consume(ConsumeContext<TCommand> context)
    {
        await HandleCommandAsync(context);
    }

    public abstract Task HandleCommandAsync(ConsumeContext<TCommand> context);
}

public abstract class CommandHandlerBase<TCommand, TResult> : ICommandHandler<TCommand> 
    where TCommand : class, ICommand
    where TResult : class
{
    public async Task Consume(ConsumeContext<TCommand> context)
    {
        var result = await HandleCommandAsync(context);

        await context.RespondAsync<TResult>(result);
    }

    public abstract Task<TResult> HandleCommandAsync(ConsumeContext<TCommand> context);
}
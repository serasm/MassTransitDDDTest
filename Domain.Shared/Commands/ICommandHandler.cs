using MassTransit;

namespace Domain.Shared.Commands;

public interface ICommandHandler<in TCommand> : IConsumer<TCommand> 
    where TCommand : class, ICommand
{
    
}
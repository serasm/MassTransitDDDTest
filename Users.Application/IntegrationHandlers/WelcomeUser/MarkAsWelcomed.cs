using Domain.Shared.Commands;
using Domain.Shared.Database;
using Domain.Shared.Events;
using MassTransit;
using MassTransit.Mediator;
using Users.Domain;

namespace Users.Application.IntegrationHandlers.WelcomeUser;

public class MarkAsWelcomedCommand : CommandBase
{
    public UserUniqueId UniqueId { get; }

    public MarkAsWelcomedCommand(UserUniqueId uniqueId) => UniqueId = uniqueId;
}

public class MarkAsWelcomedCommandHandler : CommandHandlerBase<MarkAsWelcomedCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public MarkAsWelcomedCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    
    public override async Task HandleCommandAsync(ConsumeContext<MarkAsWelcomedCommand> context)
    {
        var usersRepository = _unitOfWork.GetRepository<User, UserUniqueId>();
        
        var user = await usersRepository.FindAsync(user => user.UniqueId == context.Message.UniqueId);

        user.MarkAsWelcomed();
        
        await usersRepository.UpdateAsync(user);

        await _unitOfWork.CommitAsync();
    }
}
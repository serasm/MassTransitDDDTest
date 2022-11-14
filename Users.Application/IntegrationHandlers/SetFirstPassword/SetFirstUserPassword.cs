using Domain.Shared.Commands;
using Domain.Shared.Database;
using Domain.Shared.Events;
using MassTransit;
using MassTransit.Mediator;
using Users.Domain;

namespace Users.Application.IntegrationHandlers.SetFirstPassword;

public class SetFirstUserPasswordCommand : CommandBase
{
    public UserUniqueId UniqueId { get; }
    public string Password { get; }

    public SetFirstUserPasswordCommand(
        UserUniqueId uniqueId,
        string password)
    {
        UniqueId = uniqueId;
        Password = password;
    }
}

public class SetFirstUserPasswordCommandHandler : CommandHandlerBase<SetFirstUserPasswordCommand>
{
    private IUnitOfWork _unitOfWork;

    public SetFirstUserPasswordCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    
    public override async Task HandleCommandAsync(ConsumeContext<SetFirstUserPasswordCommand> context)
    {
        var usersRepository = _unitOfWork.GetRepository<User, UserUniqueId>();

        var user = await usersRepository.FindAsync(s => s.UniqueId == context.Message.UniqueId);

        user.ChangePassword(context.Message.Password);

        await usersRepository.UpdateAsync(user);

        await _unitOfWork.CommitAsync();
    }
}
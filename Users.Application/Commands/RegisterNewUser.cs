using Domain.Shared.Commands;
using Domain.Shared.Database;
using FluentValidation;
using MassTransit;
using Users.Domain;

namespace Users.Application.Commands;

public class RegisteredUserDataDto
{
    public int Id;

    public RegisteredUserDataDto(int id)
    {
        Id = id;
    }
}

public class RegisterNewUserCommand : CommandBase
{
    public string Email { get; }
    public string UserName { get; }
    
    public RegisterNewUserCommand(
        string email,
        string userName) : base()
    {
        Email = email;
        UserName = userName;
    }
}

public class RegisterNewUserCommandValidator : AbstractValidator<RegisterNewUserCommand>
{
    public RegisterNewUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotNull()
                .WithMessage("Email cannot be null!")
            .NotEmpty()
                .WithMessage("Email cannot be empty!")
            .NotEqual(string.Empty)
                .WithMessage("Email cannot be empty!");
        
        RuleFor(x => x.UserName)
            .Cascade(CascadeMode.Stop)
            .NotNull()
                .WithMessage("UserName cannot be null!")
            .NotEmpty()
                .WithMessage("UserName cannot be empty!")
            .NotEqual(string.Empty)
                .WithMessage("UserName cannot be empty!");
    }
}

public class RegisterNewUserCommandHandler : CommandHandlerBase<RegisterNewUserCommand, RegisteredUserDataDto>
{
    private readonly IEmailAddressUniqueChecker _emailAddressUniqueChecker;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterNewUserCommandHandler(
        IEmailAddressUniqueChecker emailAddressUniqueChecker,
        IUnitOfWork unitOfWork
    )
    {
        this._emailAddressUniqueChecker = emailAddressUniqueChecker;
        this._unitOfWork = unitOfWork;
    }
    
    public override async Task<RegisteredUserDataDto> HandleCommandAsync(ConsumeContext<RegisterNewUserCommand> context)
    {
        var user = User.Register(context.Message.Email, context.Message.UserName, this._emailAddressUniqueChecker);

        var usersRepository = _unitOfWork.GetRepository<User, UserUniqueId>();
            
        await usersRepository.AddAsync(user);

        var ww = await this._unitOfWork.CommitAsync();

        return new RegisteredUserDataDto(user.Id);
    }
}
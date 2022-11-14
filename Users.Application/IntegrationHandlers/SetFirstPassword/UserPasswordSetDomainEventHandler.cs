using Domain.Shared.Events.DomainEvents;
using MassTransit;
using MassTransit.Mediator;
using Users.Application.Configuration.Services;
using Users.Domain;
using Users.Domain.DomainEvents;

namespace Users.Application.IntegrationHandlers.SetFirstPassword;

public class UserPasswordSetDomainEventHandler : DomainEventHandlerBase<UserPasswordSetSimplifiedEvent>
{
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly IEmailSender _emailSender;
    private readonly IMediator _mediator;
    
    public UserPasswordSetDomainEventHandler(
        IPasswordGenerator passwordGenerator,
        IEmailSender emailSender,
        IMediator mediator)
    {
        _passwordGenerator = passwordGenerator;
        _emailSender = emailSender;
        _mediator = mediator;
    }
    
    public override async Task HandleEventAsync(ConsumeContext<UserPasswordSetSimplifiedEvent> context)
    {
        Console.WriteLine("Generowanie hasla");
        // generate password
        var passwd = this._passwordGenerator.Generate();

        await this._emailSender.SendEmail();

        await _mediator.Send<SetFirstUserPasswordCommand>(
            new SetFirstUserPasswordCommand(new UserUniqueId(context.Message.UserUniqueId), passwd));
    }
}
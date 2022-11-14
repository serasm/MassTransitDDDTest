using Domain.Shared.Events.DomainEvents;
using MassTransit;
using MassTransit.Mediator;
using Users.Application.Configuration.Services;
using Users.Domain;
using Users.Domain.DomainEvents;

namespace Users.Application.IntegrationHandlers.WelcomeUser;

public class UserRegistrationDomainEventHandler : DomainEventHandlerBase<UserRegisteredSimplifiedEvent>
{
    private IEmailSender _emailSender;
    private IMediator _mediator;

    public UserRegistrationDomainEventHandler(
        IEmailSender emailSender,
        IMediator mediator)
    {
        _emailSender = emailSender;
        _mediator = mediator;
    }
    
    public override async Task HandleEventAsync(ConsumeContext<UserRegisteredSimplifiedEvent> context)
    {
        Console.WriteLine("Wysylanie maila");
        await this._emailSender.SendEmail();

        await _mediator.Send<MarkAsWelcomedCommand>(new MarkAsWelcomedCommand(new UserUniqueId(context.Message.UserUniqueId)));
    }
}
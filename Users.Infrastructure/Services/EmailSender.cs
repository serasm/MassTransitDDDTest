using Users.Application.Configuration.Services;

namespace Users.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    public Task SendEmail()
    {
        return Task.CompletedTask;
    }
}
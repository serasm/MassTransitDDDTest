namespace Users.Application.Configuration.Services;

public interface IEmailSender
{
    Task SendEmail();
}
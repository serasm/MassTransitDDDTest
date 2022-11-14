using Autofac;
using Users.Application.Configuration.Services;

namespace Users.Infrastructure.Services;

public class ServicesModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EmailSender>()
            .As<IEmailSender>()
            .InstancePerLifetimeScope();

        builder.RegisterType<PasswordGenerator>()
            .As<IPasswordGenerator>()
            .InstancePerLifetimeScope();
    }
}
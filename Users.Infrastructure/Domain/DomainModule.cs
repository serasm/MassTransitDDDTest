using Autofac;
using Users.Domain;

namespace Users.Infrastructure.Domain;

public class DomainModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EmailAddressUniqueChecker>()
            .As<IEmailAddressUniqueChecker>()
            .InstancePerLifetimeScope();
    }
}
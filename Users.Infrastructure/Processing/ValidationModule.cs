using Autofac;
using FluentValidation;
using Users.Application;
using Users.Domain;

namespace Users.Infrastructure.Processing;

public class ValidationModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(UserApplicationAssembly).Assembly)
            .AsClosedTypesOf(typeof(IValidator<>))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
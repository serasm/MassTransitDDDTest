using Autofac;
using Users.Database;
using Users.Infrastructure.Database;
using Users.Infrastructure.Domain;
using Users.Infrastructure.Processing;
using Users.Infrastructure.Services;

namespace Users.Infrastructure;

public enum ApplicationType
{
    Api,
    MessageQueue
}

public class ApplicationStartup
{
    public static void Initialize(
        ContainerBuilder container,
        string connectionString)
    {
        container.RegisterModule(new DatabaseModule(connectionString));
        container.RegisterModule(new DataAccessModule(connectionString));
        container.RegisterModule(new DomainModule());
        container.RegisterModule(new ProcessingModule());
        container.RegisterModule(new ServicesModule());
        container.RegisterModule(new ValidationModule());
    }
}
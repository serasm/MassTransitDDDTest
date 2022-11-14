using Autofac;
using Domain.Shared.Database;
using Microsoft.EntityFrameworkCore;
using Users.Infrastructure.Domain;

namespace Users.Infrastructure.Database;

public class DataAccessModule : Autofac.Module
{
    private readonly string _databaseConnectionString;
    
    public DataAccessModule(string databaseConnectionString)
    {
        _databaseConnectionString = databaseConnectionString;
    }
    
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SqlConnectionFactory>()
            .As<ISqlConnectionFactory>()
            .WithParameter("connectionString", _databaseConnectionString)
            .InstancePerLifetimeScope();

        builder.RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();

        
    }
}
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Users.Database.SeedWork;

namespace Users.Database;

public class DatabaseModule : Autofac.Module
{
    private readonly string _databaseConnectionString;
    
    public DatabaseModule(string databaseConnectionString)
    {
        _databaseConnectionString = databaseConnectionString;
    }
    
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<StronglyTypedIdValueConverterSelector>()
            .As<IValueConverterSelector>()
            .InstancePerLifetimeScope();
        
        builder
            .Register(c =>
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<UsersContext>();
                dbContextOptionsBuilder.UseNpgsql(_databaseConnectionString);
                dbContextOptionsBuilder
                    .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();
                
                return new UsersContext(dbContextOptionsBuilder.Options);
            })
            .AsSelf()
            .As<DbContext>()
            .InstancePerLifetimeScope();
    }
}
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Users.Database;

namespace Users.Migrations
{
    public class Program
    {
        private const string ConnectionString = "DefaultConnection";
        
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>((hostContext,builder)=>
                {
                    var connString = hostContext.Configuration.GetConnectionString(ConnectionString);
                    
                    builder.RegisterModule(
                        new DatabaseModule(connString));
                });
    }
}
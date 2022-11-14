using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Users.Infrastructure;
using Users.Infrastructure.Processing;
using Users.Infrastructure.Processing.MassTransitExtensions;

namespace Users.MessageQueue
{
    public class Program
    {
        private const string ConnectionString = "DefaultConnection";
        private const string RabbitMqConfiguration = "MessageQueueConfiguration";
        
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                    services.LoadAssemblies();
                    
                    services.RegisterMediatorWithConsumers();
                    services.RegisterMessageQueueWithConsumers(
                        hostContext.Configuration.GetSection(RabbitMqConfiguration).Get<RabbitMqConfig>());
                    services.AddHostedService<Worker>();
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>((hostContext,builder)=>
                {
                    ApplicationStartup.Initialize(builder,
                        hostContext.Configuration.GetConnectionString(ConnectionString));
                });
    }
}
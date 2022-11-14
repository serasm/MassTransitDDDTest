using System;
using Autofac;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Users.Infrastructure;
using Users.Infrastructure.Processing;
using Users.Infrastructure.Processing.MassTransitExtensions;

namespace Users.Api
{
    public class Startup
    {
        private const string ConnectionString = "DefaultConnection";
        private const string RabbitMqConfiguration = "MessageQueueConfiguration";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Users.Api", Version = "v1" });
            });
            
            services.LoadAssemblies();

            services.RegisterMediatorWithConsumers();
            services.RegisterMessageQueueWithoutConsumers(
                Configuration.GetSection(RabbitMqConfiguration).Get<RabbitMqConfig>());
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // TODO: dodaj konfiguracjê kontenera
            ApplicationStartup.Initialize(builder, 
                Configuration.GetConnectionString(ConnectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users.Api v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
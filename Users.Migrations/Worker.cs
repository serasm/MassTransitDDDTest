using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Users.Database;

namespace Users.Migrations
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly UsersContext _context;
        private readonly IHostApplicationLifetime _lifetime;

        public Worker(ILogger<Worker> logger, UsersContext context, IHostApplicationLifetime lifetime)
        {
            _logger = logger;
            _context = context;
            _lifetime = lifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Applying Migration");

                    await _context.Database.MigrateAsync(stoppingToken);

                    _logger.LogInformation("Done");

                    stoppingToken = new CancellationToken(true);

                    var t = stoppingToken.IsCancellationRequested;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _lifetime.StopApplication();
            }
        }
    }
}
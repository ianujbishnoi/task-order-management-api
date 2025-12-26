using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrderManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Hosting; 

namespace TaskOrderManagement.Infrastructure.BackgroundJobs
{
    public class TaskExpirationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<TaskExpirationService> _logger;

        public TaskExpirationService(
            IServiceScopeFactory scopeFactory,
            ILogger<TaskExpirationService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var context = scope.ServiceProvider
                        .GetRequiredService<ApplicationDbContext>();

                    var expiryDate = DateTime.UtcNow.AddDays(-7);

                    var tasks = await context.Tasks
                        .Where(t => !t.IsCompleted && !t.IsExpired && t.CreatedAt < expiryDate)
                        .ToListAsync(stoppingToken);

                    if (tasks.Any())
                    {
                        foreach (var task in tasks)
                            task.IsExpired = true;

                        await context.SaveChangesAsync(stoppingToken);

                        _logger.LogInformation(
                            "Expired {Count} tasks", tasks.Count);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in TaskExpirationService");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}

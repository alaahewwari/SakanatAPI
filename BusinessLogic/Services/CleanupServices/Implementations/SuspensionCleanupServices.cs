using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services.CleanupServices.Implementations
{
    public class SuspensionCleanupServices(
        IServiceScopeFactory scopeFactory,
        ILogger<SuspensionCleanupServices> logger)
        : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("SuspensionCleanupServices started.");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); 
                    await RemoveExpiredSuspensionsOrDiscounts();
                }
                catch (Exception ex)
                {
                    logger.LogError($"An error occurred during the execution of RemoveExpiredSuspensions: {ex.ToString()}");
                    // Handle or rethrow the exception if critical and you want to stop the service.
                }
            }
            logger.LogInformation("SuspensionCleanupServices stopped.");
        }
        private async Task RemoveExpiredSuspensionsOrDiscounts()
        {
            try
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var currentDate = DateOnly.FromDateTime(DateTime.Now);
                    var expiredSuspensions =await dbContext.Suspensions.Where(s => s.EndDate <= currentDate).ToListAsync();
                    var expiredDiscounts =await dbContext.ApartmentDiscounts.Where(d => d.ExpiresAt <= currentDate).ToListAsync();
                    var terminatedContracts = await dbContext.Contracts
                        .Where(c => c.PaymentLogs.Sum(a => a.Amount) == c.RentPrice)
                        .ToListAsync();

                    terminatedContracts.ForEach(c => c.IsTerminated = true);
                    dbContext.Suspensions.RemoveRange(expiredSuspensions);
                    dbContext.ApartmentDiscounts.RemoveRange(expiredDiscounts);
                    await dbContext.SaveChangesAsync();

                    logger.LogInformation($"Removed {expiredSuspensions.Count} expired suspensions and {expiredDiscounts.Count} expired discounts.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to remove expired suspensions or discounts: {ex.ToString()}");
                // Decide if you need to rethrow the exception or handle it based on your application's requirements
            }
        }
    }
    }

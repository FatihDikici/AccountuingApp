
using AccountingSystem.Api.Context;
using AccountingSystem.Api.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystem.Api.Services;

public class OverdueInvoiceNotifier(
    ILogger<OverdueInvoiceNotifier> logger,
    IServiceProvider serviceProvider,
    IConfiguration configuration) : BackgroundService
{
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var interval = configuration.GetSection("OverdueCheck").GetValue<int>("IntervalSeconds", 60);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var hub = scope.ServiceProvider.GetRequiredService<IHubContext<AccountingHub>>();

                var now = DateTime.UtcNow;

                var overdue = await db.Invoices
                    .Where(i => !i.IsPaid && i.DueDate < now)
                    .Select(i => new
                    {
                        i.Id,
                        i.DueDate,
                        i.CustomerId,
                        i.PaidAmount,
                        i.TotalAmount,

                    }).ToListAsync(stoppingToken);
                if (overdue.Count > 0)
                {
                    await hub.Clients.All.SendAsync("OverdueInvoiceFound", overdue, cancellationToken: stoppingToken);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Overdue check failed");
            }

            await Task.Delay(TimeSpan.FromSeconds(interval), stoppingToken);
        }
    }
}

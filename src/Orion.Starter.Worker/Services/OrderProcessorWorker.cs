namespace Orion.Starter.Worker.Services;

public sealed class OrderProcessorWorker(ILogger<OrderProcessorWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Worker heartbeat at {UtcNow}", DateTimeOffset.UtcNow);
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}

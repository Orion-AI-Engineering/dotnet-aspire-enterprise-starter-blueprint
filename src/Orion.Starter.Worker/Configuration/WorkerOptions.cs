namespace Orion.Starter.Worker.Configuration;

public sealed class WorkerOptions
{
    public int PollingIntervalSeconds { get; init; } = 10;
}

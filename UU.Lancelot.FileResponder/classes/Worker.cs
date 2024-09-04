namespace UU.Lancelot.FileResponder;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        WatchDirectory watchDirectory = new WatchDirectory();

        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("funguju" + DateTime.Now);
                watchDirectory.pathFileChangedEventHandler += Delete.Delete_EventHandler;
                watchDirectory.StartWatchingDirectory();
            }
            await Task.Delay(5000, stoppingToken);
        }

        watchDirectory.Dispose();
    }
}






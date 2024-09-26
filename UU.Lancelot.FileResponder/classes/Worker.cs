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
        using (WatchDirectory watchDirectory = new WatchDirectory())
        {
            watchDirectory.pathFileChangedEventHandler += Delete.Delete_EventHandler;
            watchDirectory.StartWatchingDirectory();

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("funguju " + DateTime.Now);
            }
            //wait till its not finished or stoppingToken. ! for warrning
            await watchDirectory.task!.WaitAsync(stoppingToken);
        }
    }
}






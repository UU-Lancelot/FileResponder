using UU.Lancelot.FileResponder.Configuration;
using UU.Lancelot.FileResponder.Watch;

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
        var tasks = new List<Task>();

        foreach (InstanceConfiguration instance in InstanceConfiguration.LoadInstances())
        {
            tasks.Add(Task.Run(async () =>
            {
                using (WatchDirectory watchDirectory = new WatchDirectory(instance.InputDir!))
                {
                    watchDirectory.pathFileChangedEventHandler += Delete.Delete_EventHandler;
                    watchDirectory.StartWatchingDirectory();

                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        _logger.LogInformation("funguju " + DateTime.Now + " " + instance.InputDir);
                    }
                    //wait till its not finished or stoppingToken. ! for warning
                    await watchDirectory.task!.WaitAsync(stoppingToken);
                }
            }, stoppingToken));
        }

        await Task.WhenAll(tasks);
    }
}

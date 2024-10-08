using System.Xml.Serialization;
using UU.Lancelot.FileResponder.Configuration;
using UU.Lancelot.FileResponder.FormatIO;
using UU.Lancelot.FileResponder.Interfaces;
using UU.Lancelot.FileResponder.Watch;

namespace UU.Lancelot.FileResponder;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker>? _logger;
    private readonly XmlFormatIO? _XmlFormatIO;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            _XmlFormatIO = scope.ServiceProvider.GetRequiredService<XmlFormatIO>();
        }

        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var tasks = new List<Task>();

        foreach (InstanceConfiguration instance in InstanceConfiguration.LoadInstances())
        {
            tasks.Add(Task.Run(async () =>
            {
                using (WatchDirectory watchDirectory = new WatchDirectory(instance))
                {
                    watchDirectory.pathFileChangedEventHandler += _XmlFormatIO.Format_EventHandler;
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

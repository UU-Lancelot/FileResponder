using UU.Lancelot.FileResponder.Configuration;
using UU.Lancelot.FileResponder.FormatIO;
using UU.Lancelot.FileResponder.Watch;

namespace UU.Lancelot.FileResponder;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var tasks = new List<Task>();

        foreach (InstanceConfiguration instance in InstanceConfiguration.LoadInstances())
        {
            tasks.Add(Task.Run(async () =>
            {
                using (WatchDirectory watchDirectory = new WatchDirectory(instance, file => ProcessFile(instance, file)))
                {
                    watchDirectory.StartWatchingDirectory();

                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        _logger.LogInformation("funguju " + DateTime.Now + " " + instance.InputDir);
                    }
                    //wait till its not finished or stoppingToken. ! for warning
                    await watchDirectory._task!.WaitAsync(stoppingToken);
                }
            }, stoppingToken));
        }

        await Task.WhenAll(tasks);
    }

    private void ProcessFile(InstanceConfiguration config, string filePath)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            // fill the context with the file path and the instance configuration
            var context = scope.ServiceProvider.GetRequiredService<InputFileContext>();
            context.FilePath = filePath;
            context.InstanceConfiguration = config;

            // get the format IO service and format the file
            var formatIO = scope.ServiceProvider.GetRequiredService<XmlFormatIO>();

            using (var templateStream = new FileStream(context.InstanceConfiguration.TemplatePath, FileMode.Open))
            using (var outputStream = new FileStream(Path.Combine(context.InstanceConfiguration.OutputDir, Path.GetFileName(context.FilePath)), FileMode.Create))
            {
                formatIO.Format(templateStream, outputStream);
            }
        }
    }
}

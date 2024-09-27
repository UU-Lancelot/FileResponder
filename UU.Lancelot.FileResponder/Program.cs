using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using UU.Lancelot.FileResponder;


#if WINDOWS
    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
    builder.Services.AddWindowsService(options =>
    {
        options.ServiceName = "Lancelot FileResponder";
    });

    LoggerProviderOptions.RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(builder.Services);

    builder.Services.AddSingleton<Worker>();
    builder.Services.AddHostedService<Worker>();
    builder.Services.AddLogging(configure => configure.AddEventLog());

    IHost host = builder.Build();
    host.Run();
#else
    Console.WriteLine("This service can only run on Windows.");
#endif

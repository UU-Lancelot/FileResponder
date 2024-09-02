using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using UU.Lancelot.FileResponder;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = ".NET Joke Service";
});

LoggerProviderOptions.RegisterProviderOptions<
    EventLogSettings, EventLogLoggerProvider>(builder.Services);

builder.Services.AddSingleton<Worker>();
builder.Services.AddHostedService<Worker>();
builder.Services.AddLogging(configure => configure.AddEventLog());

IHost host = builder.Build();
host.Run();
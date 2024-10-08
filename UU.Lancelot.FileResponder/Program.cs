// using UU.Lancelot.FileResponder.FormatIO;

// #if WINDOWS
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using UU.Lancelot.FileResponder;
using UU.Lancelot.FileResponder.Configuration;
using UU.Lancelot.FileResponder.Replacers;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Lancelot FileResponder";
});

LoggerProviderOptions.RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(builder.Services);

builder.Services.AddSingleton<Worker>();
builder.Services.AddHostedService<Worker>();

builder.Services.AddScoped<ReplacerDatetime>();
builder.Services.AddScoped<ReplacerInput>();
builder.Services.AddScoped<ReplacerMath>();
builder.Services.AddScoped<ReplacerRandom>();
builder.Services.AddScoped<ReplacerString>();

builder.Services.AddLogging(configure => configure.AddEventLog());

List<InstanceConfiguration> instances = InstanceConfiguration.LoadInstances();


IHost host = builder.Build();
host.Run();
// #else
// Console.WriteLine("This service can only run on Windows.");
// #endif
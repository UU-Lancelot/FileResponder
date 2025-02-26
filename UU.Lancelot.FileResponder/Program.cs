#if WINDOWS
    using Microsoft.Extensions.Logging.Configuration;
    using Microsoft.Extensions.Logging.EventLog;
    using UU.Lancelot.FileResponder;
    using UU.Lancelot.FileResponder.FormatIO;
    using UU.Lancelot.FileResponder.PlaceholderProcessing;
    using UU.Lancelot.FileResponder.Replacers;
    using UU.Lancelot.FileResponder.Watch;

    HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
    builder.Services.AddWindowsService(options =>
    {
        options.ServiceName = "Lancelot FileResponder";
    });

    LoggerProviderOptions.RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(builder.Services);

    builder.Services.AddSingleton<Worker>();
    builder.Services.AddHostedService<Worker>();

    builder.Services.AddScoped<ReplacerMain>();
    builder.Services.AddScoped<ReplacerDatetime>();
    builder.Services.AddScoped<ReplacerInput>();
    builder.Services.AddScoped<ReplacerMath>();
    builder.Services.AddScoped<ReplacerRandom>();
    builder.Services.AddScoped<ReplacerString>();
    builder.Services.AddScoped<XmlFormatIO>();
    builder.Services.AddScoped<PlaceholderEvaluator>();
    builder.Services.AddScoped<InputFileContext>();

    builder.Services.AddLogging(configure => configure.AddEventLog());

    IHost host = builder.Build();
    host.Run();
#else
using UU.Lancelot.FileResponder.Configuration;

Console.WriteLine("This service can only run on Windows.");
#endif

InstanceConfiguration.LoadInstances();
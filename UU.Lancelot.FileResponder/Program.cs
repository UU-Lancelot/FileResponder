// using UU.Lancelot.FileResponder.FormatIO;

// #if WINDOWS
//     HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
//     builder.Services.AddWindowsService(options =>
//     {
//         options.ServiceName = "Lancelot FileResponder";
//     });

//     LoggerProviderOptions.RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(builder.Services);

//     builder.Services.AddSingleton<Worker>();
//     builder.Services.AddHostedService<Worker>();
//     builder.Services.AddLogging(configure => configure.AddEventLog());

//     IHost host = builder.Build();
//     host.Run();
// #else
// Console.WriteLine("This service can only run on Windows.");
// #endif

using UU.Lancelot.FileResponder.Replacers;

ReplacerIf replacer = new ReplacerIf();
string[] parameters = { "test", "test", "test" };

IEnumerable<object> result = replacer.ReplaceBlock("..", "AreEqual", parameters);

foreach (var item in result)
{
    Console.WriteLine(item); // Output: True
}
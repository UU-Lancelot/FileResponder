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

// v replacerIf to bude vzdy zobrazovat block?
// co s tim ma delat 



using UU.Lancelot.FileResponder.FormatIO;
using UU.Lancelot.FileResponder.PlaceholderProcessing;

string placeholder = @"{{{ If.AreEqual(Random.IntRange(0, 6), 0) }} <Looser /> }}}";

XmlFormatIO xmlFormatIO = new XmlFormatIO();

string filepath = @"..\Examples\template.xml";
string resultpath = @"C:\Users\marek\Desktop\result.xml";

using (FileStream fileStream = new FileStream(filepath, FileMode.Open))
using (FileStream resultStream = new FileStream(resultpath, FileMode.Create))
{
    xmlFormatIO.Format(fileStream, resultStream);
}
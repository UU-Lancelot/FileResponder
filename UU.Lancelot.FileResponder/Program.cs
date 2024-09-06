using UU.Lancelot.FileResponder.FormatIO;
using UU.Lancelot.FileResponder.Interfaces;
using UU.Lancelot.FileResponder.Replacers;
using UU.Lancelot.FileResponder.Classes;

// var builder = Host.CreateApplicationBuilder(args);
// builder.Services.AddHostedService<Worker>();

// var host = builder.Build();
// host.Run();

// TEST //
IReplacer replacer = new PseudoReplacer();
IFormatIO formatIO = new XmlFormatIO();
string templateContent = File.ReadAllText("../Examples/template.xml");

string result = formatIO.Format(templateContent, replacer);
//Console.WriteLine(result);

FindPlaceholdersXml findPlaceholdersXml = new FindPlaceholdersXml();

findPlaceholdersXml.Read(templateContent);
// END TEST //
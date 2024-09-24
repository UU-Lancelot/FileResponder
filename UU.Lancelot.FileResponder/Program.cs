using UU.Lancelot.FileResponder.FormatIO;
using UU.Lancelot.FileResponder.Interfaces;
using UU.Lancelot.FileResponder.Replacers;

// var builder = Host.CreateApplicationBuilder(args);
// builder.Services.AddHostedService<Worker>();

// var host = builder.Build();
// host.Run();

// TEST //
IReplacer replacer = new PseudoReplacer();
IFormatIO formatIO = new XmlFormatIO();
FileStream templateContent = File.OpenRead("../Examples/template.xml");
FileStream resultContent = File.Create("../Examples/result.xml");
XmlFormatIO XmlFormatIO = new XmlFormatIO();

//formatIO.Format(templateContent, resultContent, replacer);
//Console.WriteLine(result);


//XmlFormatIO.Read(templateContent);


//XmlFormatIO.ReadXml(templateContent, @"C:\Users\marek\Desktop\New Text Document.txt");

XmlFormatIO.Format(templateContent, resultContent, replacer);

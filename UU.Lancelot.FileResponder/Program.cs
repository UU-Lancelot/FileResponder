using UU.Lancelot.FileResponder.FormatIO;
using UU.Lancelot.FileResponder.Interfaces;
using UU.Lancelot.FileResponder.Replacers;

using Microsoft.Extensions.Configuration;


// TEST //
// IReplacer replacer = new PseudoReplacer();
// IFormatIO formatIO = new XmlFormatIO();
// string templateContent = File.ReadAllText("../Examples/template.xml");

// string result = formatIO.Format(templateContent, replacer);
// //Console.WriteLine(result);

// XmlFormatIO XmlFormatIO = new XmlFormatIO();

//XmlFormatIO.Read(templateContent);


//XmlFormatIO.ReadXml(templateContent, @"C:\Users\marek\Desktop\New Text Document.txt");

// ReplacerMain replacerMain = new ReplacerMain();

// string result1 = replacerMain.ReplaceValue("Random.ReplaceIntValue");
// string result2 = replacerMain.ReplaceValue("Random.ReplaceStringValue");

// Console.WriteLine(result1);
// Console.WriteLine(result2);


InstanceSettings.LoadInstances();
InstanceSettings.Instances?.ForEach(x => Console.WriteLine(x.InputDir));

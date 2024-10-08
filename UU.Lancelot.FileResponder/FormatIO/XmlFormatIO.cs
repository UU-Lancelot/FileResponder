using UU.Lancelot.FileResponder.Interfaces;
using System.Text.RegularExpressions;
using System.Xml;
using UU.Lancelot.FileResponder.Replacers;
using UU.Lancelot.FileResponder.PlaceholderProcessing;
using UU.Lancelot.FileResponder.Configuration;

namespace UU.Lancelot.FileResponder.FormatIO;
class XmlFormatIO : IFormatIO
{
    PlaceholderEvaluator? placeholderEvaluator;

    public XmlFormatIO(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            placeholderEvaluator = scope.ServiceProvider.GetRequiredService<PlaceholderEvaluator>();
        }
    }
    public void Format_EventHandler(object sender, (string, InstanceConfiguration) args)
    {
        string file = args.Item1;
        InstanceConfiguration instanceConfiguration = args.Item2;
        string outputFilePath = instanceConfiguration.OutputDir + file.Substring(instanceConfiguration.InputDir.Length);

        try
        {
            using (var input = new FileStream(file, FileMode.Open))
            using (var output = new FileStream(outputFilePath, FileMode.Create))
            {
                Format(input, output);
            }
        }

        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }

    public void Format(Stream fileContent, Stream resultContent)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(fileContent);

        using (StreamWriter streamWriter = new StreamWriter(resultContent))
        using (XmlWriter xmlWriter = XmlWriter.Create(streamWriter))
        {
            Regex regex = new Regex(@"\{\{(.*)\}\}");

            ProcessXmlNode(xmlDocument.DocumentElement, regex);

            xmlDocument.WriteTo(xmlWriter);
        }
    }

    private void ProcessXmlNode(XmlNode? xmlNode, Regex regex)
    {
        if (xmlNode == null)
        { return; }

        if (xmlNode.Attributes != null)
        {
            foreach (XmlAttribute attribute in xmlNode.Attributes)
            {
                if (regex.IsMatch(attribute.Value))
                {
                    attribute.Value = ReplaceValue(attribute.Value);
                }
            }
        }

        if (xmlNode.NodeType == XmlNodeType.Text && xmlNode.Value != null)
        {
            if (regex.IsMatch(xmlNode.Value))
            {
                xmlNode.Value = ReplaceValue(xmlNode.Value);
            }
        }

        foreach (XmlNode childNode in xmlNode.ChildNodes)
        {
            ProcessXmlNode(childNode, regex);
        }
    }

    public string ReplaceValue(string value)
    {
        return placeholderEvaluator.Evaluate(value);
    }
}
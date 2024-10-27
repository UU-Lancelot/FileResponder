using UU.Lancelot.FileResponder.Interfaces;
using System.Text.RegularExpressions;
using System.Xml;
using UU.Lancelot.FileResponder.Replacers;
using UU.Lancelot.FileResponder.PlaceholderProcessing;

namespace UU.Lancelot.FileResponder.FormatIO;
public class XmlFormatIO : IFormatIO
{
    PlaceholderEvaluator placeholderEvaluator = new PlaceholderEvaluator();
    public void Format(Stream fileContent, Stream resultContent)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(fileContent);

        using (StreamWriter streamWriter = new StreamWriter(resultContent))
        using (XmlWriter xmlWriter = XmlWriter.Create(streamWriter))
        {
            Regex regexSimplePlaceholder = new Regex(@"\{\{(.*)\}\}");
            Regex regexBlockPlaceholder = new Regex(@"\{\{\{(.*)\}\}\}");

            ProcessXmlNode(xmlDocument.DocumentElement, regexSimplePlaceholder, regexBlockPlaceholder);

            xmlDocument.WriteTo(xmlWriter);
        }
    }

    private void ProcessXmlNode(XmlNode? xmlNode, Regex regexSimple, Regex regexBlock)
    {
        if (xmlNode == null)
        { return; }

        if (ContaintBlock(xmlNode.InnerXml))
        {
            xmlNode.InnerXml = ReplaceValue(xmlNode.InnerXml);
        }

        if (xmlNode.Attributes != null)
        {
            foreach (XmlAttribute attribute in xmlNode.Attributes)
            {
                if (regexSimple.IsMatch(attribute.Value))
                {
                    attribute.Value = ReplaceValue(attribute.Value);
                }
            }
        }

        if (xmlNode.NodeType == XmlNodeType.Text && xmlNode.Value != null)
        {
            if (regexSimple.IsMatch(xmlNode.Value))
            {
                xmlNode.Value = ReplaceValue(xmlNode.Value);
            }
        }

        foreach (XmlNode childNode in xmlNode.ChildNodes)
        {
            ProcessXmlNode(childNode, regexSimple, regexBlock);
        }
    }

    public string ReplaceValue(string value)
    {
        return placeholderEvaluator.Evaluate(value);
    }

    bool ContaintBlock(string value)
    {
        return value.StartsWith("{{{") && value.EndsWith("}}}");
    }
}
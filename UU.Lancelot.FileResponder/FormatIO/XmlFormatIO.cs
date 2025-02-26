using System.Text.RegularExpressions;
using System.Xml;
using UU.Lancelot.FileResponder.Interfaces;
using UU.Lancelot.FileResponder.PlaceholderProcessing;

namespace UU.Lancelot.FileResponder.FormatIO;
class XmlFormatIO : IFormatIO
{
    private readonly PlaceholderEvaluator _placeholderEvaluator;

    public XmlFormatIO(PlaceholderEvaluator placeholderEvaluator)
    {
        _placeholderEvaluator = placeholderEvaluator;
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
        return _placeholderEvaluator.Evaluate(value);
    }
}
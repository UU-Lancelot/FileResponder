using UU.Lancelot.FileResponder.Interfaces;
using System.Text.RegularExpressions;
using System.Xml;
using UU.Lancelot.FileResponder.Replacers;


namespace UU.Lancelot.FileResponder.FormatIO;
public class XmlFormatIO : IFormatIO
{
    static ReplacerMain replacerMain = new ReplacerMain();
    public void Format(Stream fileContent, Stream resultContent, IReplacer replacer)
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
                    attribute.Value = TrimAndReplaceValue(attribute.Value);
                }
            }
        }

        if (xmlNode.NodeType == XmlNodeType.Text && xmlNode.Value != null)
        {
            if (regex.IsMatch(xmlNode.Value))
            {
                xmlNode.Value = TrimAndReplaceValue(xmlNode.Value);
            }
        }

        foreach (XmlNode childNode in xmlNode.ChildNodes)
        {
            ProcessXmlNode(childNode, regex);
        }
    }

    public string TrimAndReplaceValue(string value)
    {
        value.Trim('{', ' ', '}');
        string[] parts = value.Split('.', 3, StringSplitOptions.RemoveEmptyEntries);
        return replacerMain.ReplaceValue(parts);
    }
}
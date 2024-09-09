using UU.Lancelot.FileResponder.Interfaces;
using System.Text.RegularExpressions;
using System.Xml;


namespace UU.Lancelot.FileResponder.FormatIO;
public class XmlFormatIO : IFormatIO
{
    public string Format(string fileContent, IReplacer replacer)
    {
        return fileContent;
    }

    public void ReadXml(string fileContent, string filePath)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(fileContent);

        using (StreamWriter streamWriter = new StreamWriter(filePath))
        using (XmlWriter xmlWriter = XmlWriter.Create(streamWriter))
        {
            Regex regex = new Regex(@"\{\{(.*?)\}\}");

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
                    Console.WriteLine("Found placeholder in attribute: " + attribute.Value.Trim());
                    //call Format
                    attribute.Value = attribute.Value.Replace("{{", "").Replace("}}", "ahooooooooj");
                }
            }
        }

        if (xmlNode.NodeType == XmlNodeType.Text && xmlNode.Value != null)
        {
            if (regex.IsMatch(xmlNode.Value))
            {
                Console.WriteLine("Found placeholder in text: " + xmlNode.Value.Trim());
                //call Format
                xmlNode.Value = xmlNode.Value.Replace("{{", "").Replace("}}", "ahooooooooj");
            }
        }

        foreach (XmlNode childNode in xmlNode.ChildNodes)
        {
            ProcessXmlNode(childNode, regex);
        }
    }
}
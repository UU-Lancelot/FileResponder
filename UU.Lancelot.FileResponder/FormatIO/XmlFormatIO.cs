using UU.Lancelot.FileResponder.Interfaces;
using System.Text.RegularExpressions;
using System.Xml;


namespace UU.Lancelot.FileResponder.FormatIO;
public class XmlFormatIO : IFormatIO
{
    public void Format(Stream fileContent, Stream resultContent, IReplacer replacer)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(fileContent);

        using (StreamWriter streamWriter = new StreamWriter(resultContent))
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
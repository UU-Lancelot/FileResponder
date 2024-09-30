using System.Xml;
using System.Xml.XPath;


namespace UU.Lancelot.FileResponder.Replacers;

class ReplacerInput : PseudoReplacer
{
    public new string ReplaceValue(string placeholder)
    {
        string[] parts = SplitToMethodAndParameters(placeholder);
        string method = parts[0];
        string[] parameters = SplitParameter(parts[1].Trim());

        switch (method)
        {
            case "XPath":
                return XPath(parameters[0]);
            default:
                Console.WriteLine($"Input Replacer Class {method} is not implemented.");
                return "";
        }
    }

    string XPath(string expression)
    {
        string path = @"..\Examples\test.xml";
        XPathDocument docNav = new XPathDocument(path);
        XPathNavigator nav = docNav.CreateNavigator();
        string strExpression = ReplaceSquareBrackets(expression.Trim('"'));

        XPathNodeIterator NodeIter = nav.Select(strExpression);
        if (NodeIter.MoveNext())
        {
            return NodeIter.Current?.Value ?? "";
        }
        
        return "";
    }

    string ReplaceSquareBrackets(string value)
    {
        return value.Replace("[", "/@").Replace("]", "");
    }
}
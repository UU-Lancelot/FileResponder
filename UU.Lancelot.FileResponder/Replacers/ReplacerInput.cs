using System.Xml;
using System.Xml.XPath;
using UU.Lancelot.FileResponder.Interfaces;


namespace UU.Lancelot.FileResponder.Replacers;

class ReplacerInput : IReplacer
{
    public string ReplaceValue(string className, string methodName, string[] parameters)
    {
        switch (methodName)
        {
            case "XPath":
                return XPath(parameters[0]);
            default:
                Console.WriteLine($"Input Replacer Class {methodName} is not implemented.");
                return "";
        }
    }

    string XPath(string expression)
    {
        string path = @"..\Examples\test.xml";
        XPathDocument docNav = new XPathDocument(path);
        XPathNavigator nav = docNav.CreateNavigator();
        string strExpression = expression.Trim('"');

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

    public IEnumerable<object> ReplaceBlock(string placeholder)
    {
        throw new NotImplementedException();
    }
}
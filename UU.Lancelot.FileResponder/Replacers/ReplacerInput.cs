using System.Xml.XPath;
using UU.Lancelot.FileResponder.Interfaces;
using UU.Lancelot.FileResponder.Watch;


namespace UU.Lancelot.FileResponder.Replacers;

public class ReplacerInput : IReplacer
{
    private readonly InputFileContext _inputFileContext;

    public ReplacerInput(InputFileContext inputFileContext)
    {
        _inputFileContext = inputFileContext;
    }

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
        string path = _inputFileContext.FilePath;
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

    public IEnumerable<object> ReplaceBlock(string placeholder)
    {
        throw new NotImplementedException();
    }
}
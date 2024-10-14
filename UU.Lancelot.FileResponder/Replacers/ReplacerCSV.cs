using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

public class ReplacerCSV : IReplacer
{
    public string ReplaceValue(string className, string methodName, string[] parameters)
    {
        switch (methodName)
        {   
            default:
                return "";
        }
    }
    public IEnumerable<object> ReplaceBlock(string placeholder)
    {
        throw new NotImplementedException();
    }
}

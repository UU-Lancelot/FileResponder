using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

public class ReplacerCSV : IReplacer
{
    public IEnumerable<object> ReplaceBlock(string placeholder)
    {
        throw new NotImplementedException();
    }

    public string ReplaceValue(string className, string methodName, string[] parameters)
    {
        switch (methodName)
        {   
            default:
                return "";
        }
    }
}

using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

public class ReplacerIf : IBlockReplacer
{
    public IEnumerable<object> ReplaceBlock(string className, string methodName, string[] parameters, string block)
    {
        switch (methodName)
        {
            case "AreEqual":
                return AreEqual(parameters) ? new List<object> { block } : new List<object>();
            default:
                return new List<object>();
        }
    }

    public bool AreEqual(string[] parameters)
    {
        if (parameters == null || parameters.Length == 0)
        {
            return false;
        }

        string firstValue = parameters[0];
        foreach (string parameter in parameters)
        {
            if (parameter != firstValue)
            {
                return false;
            }
        }

        return true;
    }

}

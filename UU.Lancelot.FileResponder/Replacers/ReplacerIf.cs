using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

public class ReplacerIf : IBlockReplacer
{
    public string ReplaceBlock(string className, string methodName, string[] parameters, string block)
    {
        switch (methodName)
        {
            case "AreEqual":
                return AreEqual(parameters) ? block : string.Empty;
            default:
                return string.Empty;
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

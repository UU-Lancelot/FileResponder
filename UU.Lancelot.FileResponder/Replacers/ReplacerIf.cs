using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

public class ReplacerIf : IBlockReplacer
{
    public string ReplaceValue(string className, string methodName, string[] parameters)
    {
        switch (methodName)
        {   
            case "AreEqual":
                return AreEqual(parameters).ToString();
            default:
                return "";
        }
    }
    public IEnumerable<object> ReplaceBlock(string className, string methodName, string[] parameters)
    {
        string result = ReplaceValue(className, methodName, parameters);
        yield return result;
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

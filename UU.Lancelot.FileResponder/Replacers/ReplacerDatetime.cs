using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

public class ReplacerDatetime : ReplacerBase
{
    public override IEnumerable<object> ReplaceBlock(string placeholder)
    {
        throw new NotImplementedException();
    }

    public override string ReplaceValue(string className, string methodName, string[] parameters)
    {
        switch (methodName)
        {
            case "Now":
                return DateTime.Now.ToString();
            default:
                Console.WriteLine($"DateTime Replacer Class {methodName} is not implemented.");
                return "";
        }
    }
}

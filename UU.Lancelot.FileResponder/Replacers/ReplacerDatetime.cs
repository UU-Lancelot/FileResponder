using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

public class ReplacerDatetime : ReplacerBase
{
    public override IEnumerable<object> ReplaceBlock(string placeholder)
    {
        throw new NotImplementedException();
    }

    public override string ReplaceValue(string[] placeholder)
    {
        string method = placeholder[0];

        switch (method)
        {
            case "Now":
                return DateTime.Now.ToString();
            default:
                Console.WriteLine($"DateTime Replacer Class {method} is not implemented.");
                return "";
        }
    }
}

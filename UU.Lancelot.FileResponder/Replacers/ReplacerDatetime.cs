using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

public class ReplacerDatetime : PseudoReplacer
{
    public new IEnumerable<object> ReplaceBlock(string placeholder)
    {
        throw new NotImplementedException();
    }

    public new string ReplaceValue(string placeholder)
    {
        string[] parts = SplitToMethodAndParameters(placeholder);
        string method = parts[0];
        if (parts.Length > 1)
        {
            string[] parameters = SplitParameter(parts[1].Trim());
        }

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

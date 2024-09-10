using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

class ReplacerMain : IReplacer
{
    public static ReplacerRandom replacerRandom = new ReplacerRandom();

    public string ReplaceValue(string placeholder)
    {
        string[] parts = placeholder.Split('.');

        string method = parts[0];

        switch (method)
        {
            case "IntRange":
                return "IntRange";
            case "String":
                return "String";
            default:
                Console.WriteLine($"Method {method} is not implemented.");
                return "";
        }
    }

    public IEnumerable<object> ReplaceBlock(string placeholder)
    {
        yield return placeholder;
    }

}
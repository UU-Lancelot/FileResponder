using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

class ReplacerMain : IReplacer
{
    public static ReplacerRandom replacerRandom = new ReplacerRandom();

    public string ReplaceValue(string placeholder)
    {
        string[] parts = placeholder.Split('.');

        string replacerClass = parts[0];
        string replacerMethod = parts[1];

        switch (replacerClass)
        {
            case "Random":
                switch (replacerMethod)
                {
                    case "ReplaceIntValue":
                        return replacerRandom.ReplaceIntValue().ToString();
                    case "ReplaceStringValue":
                        return replacerRandom.ReplaceStringValue(10);   //random value
                    default:
                        Console.WriteLine($"Replacer Class {replacerClass} is not implemented.");
                        return "";
                }


            case "String":
                return "String";


            default:
                Console.WriteLine($"Replacer Class {replacerClass} is not implemented.");
                return "";
        }
    }

    public IEnumerable<object> ReplaceBlock(string placeholder)
    {
        yield return placeholder;
    }

}
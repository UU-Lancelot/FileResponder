using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

class ReplacerMain : IReplacer
{
    static ReplacerRandom replacerRandom = new ReplacerRandom();
    static ReplacerMath replacerMath = new ReplacerMath();
    static ReplacerString replacerString = new ReplacerString();
    static ReplacerDatetime replacerDatetime = new ReplacerDatetime();

    public string ReplaceValue(string? placeholder)
    {
        if (String.IsNullOrEmpty(placeholder))
        {
            Console.WriteLine("Placeholder is empty");
            return "";
        }
        else
        {
            string replacerClass = placeholder.Split('.')[0];
            string methodAndParameters = placeholder.Substring(replacerClass.Length + 1);
            switch (replacerClass)
            {
                case "Random":
                    return replacerRandom.ReplaceValue(methodAndParameters);
                case "Math":
                    return replacerMath.ReplaceValue(methodAndParameters);
                case "String":
                    return replacerString.ReplaceValue(methodAndParameters);
                case "Datetime":
                    return replacerDatetime.ReplaceValue(methodAndParameters);
                default:
                    Console.WriteLine($"Replacer Class {replacerClass} is not implemented.");
                    return "";
            }
        }
    }

    public IEnumerable<object> ReplaceBlock(string placeholder)
    {
        yield return placeholder;
    }

}
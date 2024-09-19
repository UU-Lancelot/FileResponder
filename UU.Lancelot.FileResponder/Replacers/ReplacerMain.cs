using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

class ReplacerMain : IReplacer
{
    public static ReplacerRandom replacerRandom = new ReplacerRandom();
    public static ReplacerMath replacerMath = new ReplacerMath();
    public static ReplacerString replacerString = new ReplacerString();

    public string ReplaceValue(string placeholder)
    {
        string[] parts = placeholder.Split('.');

        string replacerClass = parts[0];
        string replacerMethod = parts[1];

        switch (replacerClass)
        {
            case "Random":
                return replacerRandom.ChooseMethod(replacerMethod);
            case "Math":
                return replacerMath.ChooseMethod(replacerMethod);
            case "String":
                return replacerString.ChooseMethod(replacerMethod);
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
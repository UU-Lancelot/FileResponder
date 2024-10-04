using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

class ReplacerMain : IReplacer
{
    static ReplacerRandom replacerRandom = new ReplacerRandom();
    static ReplacerMath replacerMath = new ReplacerMath();
    static ReplacerString replacerString = new ReplacerString();
    static ReplacerDatetime replacerDatetime = new ReplacerDatetime();

    public string ReplaceValue(string[] placeholder)
    {
        try
        {
            if (placeholder.Length < 3)
            {
                Console.WriteLine("Placeholder is empty");
                return "";
            }
            
            string replacerClass = placeholder[0];
            string[] methodAndParameters = placeholder.Skip(1).ToArray();
            methodAndParameters = methodAndParameters.Where(x => !string.IsNullOrEmpty(x)).ToArray();

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
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return "";
        }
    }

    public IEnumerable<object> ReplaceBlock(string placeholder)
    {
        yield return placeholder;
    }

}
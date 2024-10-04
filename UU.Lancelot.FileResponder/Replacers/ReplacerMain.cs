using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

class ReplacerMain : IReplacer
{
    static ReplacerRandom replacerRandom = new ReplacerRandom();
    static ReplacerMath replacerMath = new ReplacerMath();
    static ReplacerString replacerString = new ReplacerString();
    static ReplacerDatetime replacerDatetime = new ReplacerDatetime();

    public string ReplaceValue(string className, string methodName, string[] parameters)
    {
        try
        {
            switch (className)
            {
                case "Random":
                    return replacerRandom.ReplaceValue(className, methodName, parameters);
                case "Math":
                    return replacerMath.ReplaceValue(className, methodName, parameters);
                case "String":
                    return replacerString.ReplaceValue(className, methodName, parameters);
                case "DateTime":
                    return replacerDatetime.ReplaceValue(className, methodName, parameters);
                default:
                    Console.WriteLine($"ReplacerMain Class {className} is not implemented.");
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
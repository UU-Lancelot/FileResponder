using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

class ReplacerMain : IReplacer, IBlockReplacer
{
    static ReplacerRandom replacerRandom = new ReplacerRandom();
    static ReplacerMath replacerMath = new ReplacerMath();
    static ReplacerString replacerString = new ReplacerString();
    static ReplacerDatetime replacerDatetime = new ReplacerDatetime();
    static ReplacerInput replacerInput = new ReplacerInput();
    static ReplacerCSV replacerCSV = new ReplacerCSV();
    static ReplacerIf replacerIf = new ReplacerIf();

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
                case "Datetime":
                    return replacerDatetime.ReplaceValue(className, methodName, parameters);
                case "Input":
                    return replacerInput.ReplaceValue(className, methodName, parameters);
                case "ReplacerCSV":
                    return replacerCSV.ReplaceValue(className, methodName, parameters);
                case "ReplacerIf":
                    return replacerIf.ReplaceValue(className, methodName, parameters);
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

    public IEnumerable<object> ReplaceBlock(string className, string methodName, string[] parameters)
    {
        yield return className;
    }

}
using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

class ReplacerMain : IReplacer, IBlockReplacer
{
    static ReplacerRandom replacerRandom = new ReplacerRandom();
    static ReplacerMath replacerMath = new ReplacerMath();
    static ReplacerString replacerString = new ReplacerString();
    static ReplacerDatetime replacerDatetime = new ReplacerDatetime();
    static ReplacerInput replacerInput = new ReplacerInput();
    static ReplacerIf replacerIf = new ReplacerIf();
    static ReplacerFor replacerFor = new ReplacerFor();

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
                case "For":
                return replacerFor.ReplaceValue(className, methodName, parameters);
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

    public string ReplaceBlock(string className, string methodName, string[] parameters, string block)
    {
        switch (className)
        {
            case "For":
                return replacerFor.ReplaceBlock(className, methodName, parameters, block);
            case "If":
                return replacerIf.ReplaceBlock(className, methodName, parameters, block);

            default:
                Console.WriteLine($"ReplacerMain Class {className} is not implemented.");
                return string.Empty;
        }

    }
}
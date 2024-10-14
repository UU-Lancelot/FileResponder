using Microsoft.Extensions.DependencyInjection;
using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

class ReplacerMain : IReplacer
{
    ReplacerRandom _replacerRandom;
    ReplacerMath _replacerMath;
    ReplacerString _replacerString;
    ReplacerDatetime _replacerDatetime;
    ReplacerInput _replacerInput;
    ReplacerDataStore _replacerDataStore;

    public ReplacerMain(ReplacerDatetime replacerDatetime, ReplacerInput replacerInput, ReplacerMath replacerMath, ReplacerRandom replacerRandom, ReplacerString replacerString, ReplacerDataStore replacerDataStore)
    {
        _replacerDatetime = replacerDatetime;
        _replacerInput = replacerInput;
        _replacerMath = replacerMath;
        _replacerRandom = replacerRandom;
        _replacerString = replacerString;
        _replacerDataStore = replacerDataStore;
    }

    public string ReplaceValue(string className, string methodName, string[] parameters)
    {
        try
        {
            switch (className)
            {
                case "Random":
                    return _replacerRandom.ReplaceValue(className, methodName, parameters);
                case "Math":
                    return _replacerMath.ReplaceValue(className, methodName, parameters);
                case "String":
                    return _replacerString.ReplaceValue(className, methodName, parameters);
                case "Datetime":
                    return _replacerDatetime.ReplaceValue(className, methodName, parameters);
                case "Input":
                    return _replacerInput.ReplaceValue(className, methodName, parameters);
                case "DataStore":
                    return _replacerDataStore.ReplaceValue(className, methodName, parameters);
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
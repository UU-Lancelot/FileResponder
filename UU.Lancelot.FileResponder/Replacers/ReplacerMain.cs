using Microsoft.Extensions.DependencyInjection;
using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

class ReplacerMain : IReplacer
{
    ReplacerRandom? replacerRandom;
    ReplacerMath? replacerMath;
    ReplacerString? replacerString;
    ReplacerDatetime? replacerDatetime;
    ReplacerInput? replacerInput;

    public ReplacerMain(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            replacerDatetime = scope.ServiceProvider.GetRequiredService<ReplacerDatetime>();
            replacerInput = scope.ServiceProvider.GetRequiredService<ReplacerInput>();
            replacerMath = scope.ServiceProvider.GetRequiredService<ReplacerMath>();
            replacerRandom = scope.ServiceProvider.GetRequiredService<ReplacerRandom>();
            replacerString = scope.ServiceProvider.GetRequiredService<ReplacerString>();
        }
    }

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
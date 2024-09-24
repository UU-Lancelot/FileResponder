using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

public class ReplacerDatetime : IReplacer
{
    public IEnumerable<object> ReplaceBlock(string placeholder)
    {
        throw new NotImplementedException();
    }

    public string ReplaceValue(string placeholder)
    {
        try
        {
            string method = placeholder.Split(".")[0].Trim();

            switch (method)
            {
                case "Now":
                    return DateTime.Now.ToString();
                default:
                    Console.WriteLine($"DateTime Replacer Class {method} is not implemented.");
                    return "";
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
            return "";
        }
    }
}

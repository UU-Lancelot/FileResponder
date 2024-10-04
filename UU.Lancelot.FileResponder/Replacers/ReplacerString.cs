using System.Security.Cryptography.X509Certificates;
using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers
{
    public class ReplacerString : ReplacerBase
    {
        public override IEnumerable<object> ReplaceBlock(string placeholder)
        {
            throw new NotImplementedException();
        }

        public override string ReplaceValue(string className, string methodName, string[] parameters)
        {
            switch (methodName)
            {
                case "Concat":
                    return Concat(parameters);
                case "Repeat":
                    if (parameters.Length == 1)
                    {
                        return Repeat(parameters[0], null);
                    }
                    return Repeat(parameters[0], parameters[1]);
                default:
                    Console.WriteLine($"String Replacer Class {methodName} is not implemented.");
                    return "";
            }
        }

        string Concat(string[] words)
        {
            string result = "";
            foreach (string word in words)
            {
                result += word.Trim('"');
            }
            return result;
        }

        string Repeat(string value, string? count)
        {
            value = value.Trim('"');
            int actualCount = int.TryParse(count, out int parsedCount) ? parsedCount : 10;
            string result = "";
            for (int i = 0; i < actualCount; i++)
            {
                result += value;
            }
            return result;
        }
    }
}
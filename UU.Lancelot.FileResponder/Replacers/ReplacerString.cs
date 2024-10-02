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

        public override string ReplaceValue(string placeholder)
        {
            string[] parts = SplitToMethodAndParameters(placeholder);
            string method = parts[0];
            string[] words = SplitParameter(parts[1].Trim());


            switch (method)
            {
                case "Concat":
                    return Concat(words);
                case "Repeat":
                    if (words.Length == 1)
                    {
                        return Repeat(words[0], null);
                    }
                    return Repeat(words[0], words[1]);
                default:
                    Console.WriteLine($"String Replacer Class {method} is not implemented.");
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
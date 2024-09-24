using System.Security.Cryptography.X509Certificates;
using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers
{
    public class ReplacerString : IReplacer
    {
        public IEnumerable<object> ReplaceBlock(string placeholder)
        {
            throw new NotImplementedException();
        }

        public string ReplaceValue(string placeholder)
        {
            string[] parts = placeholder.Split(new char[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
            string method = parts[0].Trim();
            string[] words = parts.Skip(1).ToArray();
                        

            switch (method)
            {
                case "Concat":
                    return Concat(words);
                case "Repeat":
                    return Repeat(words[0]);
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

        string Repeat(string value)
        {
            string result = "";
            for (int i = 0; i < 10; i++)
            {
                result += value;
            }
            return result;
        }
    }
}
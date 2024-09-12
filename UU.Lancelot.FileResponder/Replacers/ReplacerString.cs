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
            throw new NotImplementedException();
        }

        public string ChooseMethod(string method)
        {
            switch (method)
            {
                case "ReplaceStringValue":
                    return Concat();
                case "Repeat":
                    return Repeat("Hello");
                default:
                    Console.WriteLine($"String Replacer Class {method} is not implemented.");
                    return "";
            }
        }

        string Concat()
        {
            return "Hello" + " " + "World";
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
using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers
{
    public class ReplacerMath : IReplacer
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
                case "Add":
                    return Plus(1, 2).ToString();
                case "Subtract":
                    return Minus(1, 2).ToString();
                case "Multiply":
                    return Multiply(1, 2).ToString();
                case "Divide":
                    return Divide(1, 2).ToString();
                default:
                    Console.WriteLine($"Math Replacer Class {method} is not implemented.");
                    return "";
            }
        }

        int Plus(int a, int b)
        {
            return a + b;
        }

        int Minus(int a, int b)
        {
            return a - b;
        }

        int Multiply(int a, int b)
        {
            return a * b;
        }

        int Divide(int a, int b)
        {
            return a / b;
        }
    }
}
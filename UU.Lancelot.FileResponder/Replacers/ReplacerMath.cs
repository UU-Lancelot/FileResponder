using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers
{
    public class ReplacerMath : PseudoReplacer
    {
        public new IEnumerable<object> ReplaceBlock(string placeholder)
        {
            throw new NotImplementedException();
        }

        public new string ReplaceValue(string placeholder)
        {
            string[] parts = SplitToMethodAndParameters(placeholder);
            string method = parts[0].Trim();
            string[] parameters = SplitParameter(parts[1].Trim());
            double num1 = double.Parse(parameters[0].Trim());
            double num2 = double.Parse(parameters[1].Trim());

            switch (method)
            {
                case "Add":
                    return Plus(num1, num2).ToString();
                case "Subtract":
                    return Minus(num1, num2).ToString();
                case "Multiply":
                    return Multiply(num1, num2).ToString();
                case "DivideInt":
                    var result = DivideInt(num1, num2);
                    return result?.ToString() ?? "";
                case "Divide":
                    var resultDouble = Divide(num1, num2);
                    return resultDouble?.ToString() ?? "";
                default:
                    Console.WriteLine($"Math Replacer Class {method} is not implemented.");
                    return "";
            }
        }

        double Plus(double a, double b)
        {
            return a + b;
        }

        double Minus(double a, double b)
        {
            return a - b;
        }

        double Multiply(double a, double b)
        {
            return a * b;
        }

        double? DivideInt(double a, double b)
        {
            return Math.Floor(a / b);
        }

        double? Divide(double a, double b)
        {
            return a / b;
        }
    }
}
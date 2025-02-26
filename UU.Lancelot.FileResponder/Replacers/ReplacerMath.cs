using System.Globalization;
using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers
{
    public class ReplacerMath : IReplacer
    {
        public IEnumerable<object> ReplaceBlock(string placeholder)
        {
            throw new NotImplementedException();
        }

        public string ReplaceValue(string className, string methodName, string[] parameters)
        {
            double num1 = double.Parse(parameters[0].Trim());
            double num2 = double.Parse(parameters[1].Trim());

            switch (methodName)
            {
                case "Add":
                    return Plus(num1, num2).ToString();
                case "Subtract":
                    return Minus(num1, num2).ToString();
                case "Multiply":
                    return Multiply(num1, num2).ToString();
                case "DivideInt":
                    var result = DivideInt(num1, num2);
                    return result?.ToString(CultureInfo.InvariantCulture) ?? ""; // InvariantCulture is used to avoid issues with different cultures
                case "Divide":
                    var resultDouble = Divide(num1, num2);
                    return resultDouble?.ToString(CultureInfo.InvariantCulture) ?? ""; // InvariantCulture is used to avoid issues with different cultures
                default:
                    Console.WriteLine($"Math Replacer Class {methodName} is not implemented.");
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
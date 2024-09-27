
using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers
{
    public class ReplacerRandom : IReplacer
    {
        static Random random = new Random();
        public string ReplaceValue(string placeholder)
        {
            string[] parts = placeholder.Split(new char[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
            string method = parts[0].Trim();
            double? num1 = parts.Length >= 2 ? double.Parse(parts[1].Trim()) : null;
            double? num2 = parts.Length >= 3 ? double.Parse(parts[2].Trim()) : null;


            switch (method)
            {
                case "IntRange":
                    return IntRange(num1, num2);
                case "DecimalRange":
                    return DecimalRange(num1, num2);
                case "String":
                    return StringValue(num1);
                case "Bool":
                    return BoolValue(num1);
                default:
                    Console.WriteLine($"Random Replacer Class {method} is not implemented.");
                    return "";
            }
        }
        public IEnumerable<object> ReplaceBlock(string placeholder)
        {
            yield return placeholder;
        }

        string IntRange(double? min, double? max)
        {
            // Použití výchozích hodnot, pokud jsou parametry null
            double actualMin = min ?? 100;
            double actualMax = max ?? 999;

            // +1 to include max value
            return random.NextInt64(Convert.ToInt64(actualMin), Convert.ToInt64(actualMax + 1)).ToString();
        }

        string DecimalRange(double? min, double? max)
        {
            double actualMin = min ?? 100;
            double actualMax = max ?? 999;
            //return random decimal between 0 - 1
            var result = random.NextDouble();
            //nextint returns min <= x < max
            result += random.NextInt64(Convert.ToInt64(actualMin), Convert.ToInt64(actualMax));
            return result.ToString();
        }
        string StringValue(double? length)
        {
            double actualLength = length ?? 10;
            const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string randomString = "";
            for (double i = 0; i < actualLength; i++)
            {
                randomString += CHARS[random.Next(CHARS.Length)];
            }

            return randomString;
        }

        string BoolValue(double? chanceForTrue)
        {
            double actualChance = chanceForTrue ?? 50;
            int randomInt = random.Next(1, 100);
            return (randomInt <= actualChance).ToString();
        }



    }
}
using UU.Lancelot.FileResponder.Interfaces;


namespace UU.Lancelot.FileResponder.Replacers
{
    public class ReplacerRandom : IReplacer
    {
        public IEnumerable<object> ReplaceBlock(string placeholder)
        {
            throw new NotImplementedException();
        }
        static Random random = new Random();
        public string ReplaceValue(string className, string methodName, string[] parameters)
        {
            double? num1 = parameters.Length > 0 ? double.Parse(parameters[0].Trim()) : null;
            double? num2 = parameters.Length > 1 ? double.Parse(parameters[1].Trim()) : null;

            switch (methodName)
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
                    Console.WriteLine($"Random Replacer Class {methodName} is not implemented.");
                    return "";
            }
        }
        bool FirstIsGreater(double? a, double? b)
        {
            return a >= b;
        }
        string IntRange(double? min, double? max)
        {
            double actualMin = min ?? 100;
            double actualMax = max ?? 999;
            if (FirstIsGreater(actualMin, actualMax))
            {
                throw new Exception("min should be less than or equal to max");
            }
            else
            {   // +1 to include max value
                return random.NextInt64(Convert.ToInt64(actualMin), Convert.ToInt64(actualMax + 1)).ToString();
            }
        }

        string DecimalRange(double? min, double? max)
        {
            double actualMin = min ?? 100;
            double actualMax = max ?? 999;
            //return random decimal between 0 - 1
            var result = random.NextDouble();
            //nextint returns min <= x < max

            if (FirstIsGreater(min, max))
            {
                throw new Exception("min should be less than or equal to max");
            }
            else
            {
                result += random.NextInt64(Convert.ToInt64(actualMin), Convert.ToInt64(actualMax));
                return result.ToString();
            }
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
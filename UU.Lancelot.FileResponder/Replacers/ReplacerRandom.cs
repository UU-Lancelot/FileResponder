
using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers
{
    public class ReplacerRandom : IReplacer
    {
        static Random random = new Random();
        public string ReplaceValue(string placeholder)
        {
            try
            {
                string[] parts = placeholder.Split(new char[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
                string method = parts[0].Trim();
                System.Console.WriteLine(parts[1]);
                long num1 = parts.Length >= 2 ? long.Parse(parts[1].Trim()) : 0;
                long num2 = parts.Length >= 3 ? long.Parse(parts[2].Trim()) : 0;


                switch (method)
                {
                    case "IntRange":
                        return IntValue(num1, num2).ToString();
                    case "String":
                        return StringValue(num1);
                    case "Bool":
                        return BoolValue(num1).ToString();
                    default:
                        Console.WriteLine($"Random Replacer Class {method} is not implemented.");
                        return "";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return "";
            }
        }
        public IEnumerable<object> ReplaceBlock(string placeholder)
        {
            yield return placeholder;
        }

        long IntValue(long min = 100, long max = 999)
        {
            //+1 to include max value
            return random.NextInt64(min, max + 1);
        }

        string StringValue(long length)
        {
            const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string randomString = "";
            for (long i = 0; i < length; i++)
            {
                randomString += CHARS[random.Next(CHARS.Length)];
            }

            return randomString;
        }

        bool BoolValue(long chanceForTrue = 50)
        {
            int randomInt = random.Next(1, 100);
            return randomInt <= chanceForTrue;
        }



    }
}
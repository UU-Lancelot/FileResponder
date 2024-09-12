using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers
{
    public class ReplacerRandom : IReplacer
    {
        static Random random = new Random();
        public string ReplaceValue(string placeholder)
        {
            return placeholder;
        }
        public IEnumerable<object> ReplaceBlock(string placeholder)
        {
            yield return placeholder;
        }

        public string ChooseMethod(string method)
        {
            switch (method)
            {
                case "ReplaceIntValue":
                    return IntValue().ToString();
                case "ReplaceStringValue":
                    return StringValue(10);
                case "ReplaceBoolValue":
                    return BoolValue().ToString();
                default:
                    Console.WriteLine($"Random Replacer Class {method} is not implemented.");
                    return "";
            }
        }
        int IntValue()
        {
            return random.Next(100, 999);
        }

        string StringValue(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string randomString = "";
            for (int i = 0; i < length; i++)
            {
                randomString += chars[random.Next(chars.Length)];
            }

            return randomString;
        }

        bool BoolValue()
        {
            int randomInt = random.Next(1, 100);
            return randomInt <= 60;
        }



    }
}
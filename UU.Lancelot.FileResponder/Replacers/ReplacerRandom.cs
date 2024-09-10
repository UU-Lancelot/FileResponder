using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers
{
    public class ReplacerRandom : IReplacer
    {
        public string ReplaceValue(string placeholder)
        {
            return placeholder;
        }
        public IEnumerable<object> ReplaceBlock(string placeholder)
        {
            yield return placeholder;
        }

        public int ReplaceIntValue()
        {
            Random random = new Random();
            return random.Next(0, 1000000000);
        }

        public string ReplaceStringValue(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            string randomString = "";
            for (int i = 0; i < length; i++)
            {
                randomString += chars[random.Next(chars.Length)];
            }

            return randomString;
        }

    }
}
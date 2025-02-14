using Xunit;
using UU.Lancelot.FileResponder.Replacers;

namespace UU.Lancelot.FileResponder.Test.ReplacerTests
{
    public class ReplacerRandomTest
    {
        ReplacerRandom replacer = new ReplacerRandom();
        
        [Fact]
        public void TestIntRange()
        {
            string result = replacer.ReplaceValue("Random", "IntRange", new string[] { "1", "10" });
            Assert.True(int.Parse(result) >= 1 && int.Parse(result) <= 10);
        }

        [Fact]
        public void TestDecimalRange()
        {
            string result = replacer.ReplaceValue("Random", "DecimalRange", new string[] { "0", "1" });
            Assert.True(double.Parse(result) >= 0 && double.Parse(result) <= 1);
        }

        [Fact]
        public void TestStringValue()
        {
            string result = replacer.ReplaceValue("Random", "String", new string[] { "5" });
            Assert.True(result.Length == 5);
        }

        [Fact]
        public void TestBoolValue()
        {
            string result = replacer.ReplaceValue("ReplacerRandom", "Bool", new string[] { "1" });
            Assert.True(result == "True" || result == "False");
        }
    }
}
using UU.Lancelot.FileResponder.Replacers;

namespace UU.Lancelot.FileResponder.Test.ReplacerTests
{
    public class ReplacerStringTest
    {
        ReplacerString replacer = new ReplacerString();
        
        [Fact]
        public void TestConcat()
        {
            string result = replacer.ReplaceValue("String", "Concat", new string[] { "Hello", "World" });
            Assert.Equal("HelloWorld", result);
        }

        [Fact]
        public void TestRepeat()
        {
            string result = replacer.ReplaceValue("String", "Repeat", new string[] { "Hello", "3" });
            Assert.Equal("HelloHelloHello", result);
        }
    }
}
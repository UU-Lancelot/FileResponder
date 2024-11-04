using UU.Lancelot.FileResponder.Replacers;

namespace UU.Lancelot.FileResponder.Test.ReplacerTests
{
    public class ReplacerMathTest
    {
        ReplacerMath replacer = new ReplacerMath();
        
        [Fact]
        public void TestIntRange()
        {
            string result = replacer.ReplaceValue("Math", "Add", new string[] { "1", "5" });
            Assert.Equal("6", result);
        }


        [Fact]
        public void TestSub()
        {
            string result = replacer.ReplaceValue("Math", "Subtract", new string[] { "5", "2" });
            Assert.Equal("3", result);
        }

        [Fact]
        public void TestMul()
        {
            string result = replacer.ReplaceValue("Math", "Multiply", new string[] { "5", "2" });
            Assert.Equal("10", result);
        }

        [Fact]
        public void TestDivInt()
        {
            string result = replacer.ReplaceValue("Math", "DivideInt", new string[] { "5", "2" });
            Assert.Equal("2", result);
        }

        [Fact]
        public void TestDiv()
        {
            string result = replacer.ReplaceValue("Math", "Divide", new string[] { "5", "2" });
            Assert.Equal("2.5", result);
        }
    }
}
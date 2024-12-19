using UU.Lancelot.FileResponder.PlaceholderProcessing;

namespace UU.Lancelot.FileResponder.Test
{
    public class PlaceholderEvaluatorTest
    {
        PlaceholderEvaluator placeholderEvaluator = new PlaceholderEvaluator();

        [Fact]
        public void TestMathAdd()
        {
            string result = placeholderEvaluator.Evaluate("Math.Add(1,2)");

            Assert.Equal("3", result);
        }
        [Fact]
        public void TestMathSubtract()
        {
            string result = placeholderEvaluator.Evaluate("Math.Subtract(5, 2)");

            Assert.Equal("3", result);
        }
        [Fact]
        public void TestMathDivideInt()
        {
            string result = placeholderEvaluator.Evaluate("Math.DivideInt(5, 2)");

            Assert.Equal("2", result);
        }
        [Fact]
        public void TestMathDivide()
        {
            string result = placeholderEvaluator.Evaluate("Math.Divide(5, 2)");

            Assert.Equal("2.5", result);
        }
        [Fact]
        public void TestMathMultiply()
        {
            string result = placeholderEvaluator.Evaluate("Math.Multiply(5, 2)");

            Assert.Equal("10", result);
        }
        [Fact]
        public void TestMathNested()
        {
            string result = placeholderEvaluator.Evaluate("Math.Add(1, Math.Multiply(2, 3))");

            Assert.Equal("7", result);
        }

        [Fact]
        public void TestMathNested2()
        {
            string result = placeholderEvaluator.Evaluate("Math.Add(Math.Multiply(2, 3), Math.Multiply(4, 5))");

            Assert.Equal("26", result);
        }


        [Fact]
        public void TestStringNested()
        {
            string result = placeholderEvaluator.Evaluate("String.Repeat(String.Concat(Hello, World), 2)");

            Assert.Equal("HelloWorldHelloWorld", result);
        }

        [Fact]
        public void TestStringNested2()
        {
            string result = placeholderEvaluator.Evaluate("String.Concat(String.Concat(Hello, World), String.Concat(Hello, World))");

            Assert.Equal("HelloWorldHelloWorld", result);
        }
    }
}
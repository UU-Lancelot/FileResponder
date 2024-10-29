using UU.Lancelot.FileResponder.PlaceholderProcessing;

namespace UU.Lancelot.FileResponder.Test
{
    public class UnitTest1
    {
        PlaceholderEvaluator placeholderEvaluator = new PlaceholderEvaluator();

        [Fact]
        public void MathAdd()
        {
            string result = placeholderEvaluator.Evaluate("Math.Add(1,2)");

            Assert.Equal("3", result);
        }
        [Fact]
        public void MathSubtract()
        {
            string result = placeholderEvaluator.Evaluate("Math.Subtract(5, 2)");

            Assert.Equal("3", result);
        }
        [Fact]
        public void MathDivideInt()
        {
            string result = placeholderEvaluator.Evaluate("Math.DivideInt(5, 2)");

            Assert.Equal("2", result);
        }
        [Fact]
        public void MathDivide()
        {
            string result = placeholderEvaluator.Evaluate("Math.Divide(5, 2)");

            Assert.Equal("2.5", result);
        }
        [Fact]
        public void MathMultiply()
        {
            string result = placeholderEvaluator.Evaluate("Math.Multiply(5, 2)");

            Assert.Equal("10", result);
        }
    }
}
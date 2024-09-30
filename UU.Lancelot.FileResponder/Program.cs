using  UU.Lancelot.FileResponder.PlaceholderEvaluator;

string placeholder = $"""String.Repeat("hel" + "lo", 1)""";

Console.WriteLine(PlaceholderEvaluator.Evaluate(placeholder));
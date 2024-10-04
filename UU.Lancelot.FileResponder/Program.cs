using UU.Lancelot.FileResponder.PlaceholderEvaluator;

string placeholder = $"""String.Repeat("ahoj", Math.Add(1, 10))""";
// Výsledek: ?


PlaceholderEvaluator placeholderEvaluator = new PlaceholderEvaluator();

Console.WriteLine(placeholderEvaluator.Evaluate(placeholder));
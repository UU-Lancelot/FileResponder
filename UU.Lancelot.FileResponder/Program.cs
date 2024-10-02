using  UU.Lancelot.FileResponder.PlaceholderEvaluator;

string placeholder = "Math.Add(Random.IntRange(1, 10), Random.IntRange(1, 10))";
// Výsledek: ?


PlaceholderEvaluator placeholderEvaluator = new PlaceholderEvaluator();

Console.WriteLine(placeholderEvaluator.Evaluate(placeholder));
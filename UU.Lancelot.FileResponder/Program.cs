using  UU.Lancelot.FileResponder.PlaceholderEvaluator;

string placeholder = $"""Input.XPath("CDSREQ/SenderIdentification[id]")""";

Console.WriteLine(PlaceholderEvaluator.Evaluate(placeholder));
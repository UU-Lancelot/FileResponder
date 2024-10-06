using System.IO.Compression;
using UU.Lancelot.FileResponder.Replacers;

namespace UU.Lancelot.FileResponder.PlaceholderProcessing;
public class PlaceholderEvaluator
{
    static ReplacerMain replacerMain = new ReplacerMain();
    public string Evaluate(string placeholder)
    {
        placeholder = placeholder.Trim('{', ' ', '}');

        if (string.IsNullOrEmpty(placeholder))
        {
            return "";
        }

        return ProcessPlaceholder(placeholder);

    }
    private string ProcessPlaceholder(string placeholder)
    {
        placeholder = AddDotToFirstBracket(placeholder);
        string[] parts = SplitIntoComponents(placeholder);
        parts[2] = RemoveOuterBrackets(parts[2]);

        string[] parameterArray = SplitParametr(parts[2]);

        for (int i = 0; i < parameterArray.Length; i++)
        {
            if (!IsSimpleParameter(parameterArray[i]))
            {
                parameterArray[i] = ProcessPlaceholder(parameterArray[i]);
            }
        }

        string result = replacerMain.ReplaceValue(parts[0], parts[1], parameterArray);
        placeholder.Replace(placeholder, result);
        return result;
    }
    private static string AddDotToFirstBracket(string placeholder)
    {
        int indexForDot = placeholder.IndexOf('(');
        placeholder = placeholder.Insert(indexForDot, ".");
        return placeholder;
    }
    private static string[] SplitIntoComponents(string placeholder)
    {
        string[] parts = placeholder.Split('.', 3);
        return parts;
    }
    private static string RemoveOuterBrackets(string placeholder)
    {
        placeholder = placeholder.Remove(0, 1);
        placeholder = placeholder.Remove(placeholder.Length - 1, 1);
        return placeholder;
    }
    private static string[] SplitParametr(string input)
    {
        List<string> result = new List<string>();
        int start = 0;
        int depthBracket = 0;
        bool isInQuotes = false;

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '(')
            {
                depthBracket++;
            }
            else if (input[i] == ')')
            {
                depthBracket--;
            }
            else if (input[i] == '"')
            {
                isInQuotes = !isInQuotes;
            }
            else if (input[i] == ',' && depthBracket == 0 && !isInQuotes)
            {
                result.Add(input.Substring(start, i - start));
                start = i + 1;
            }
        }
        result.Add(input.Substring(start));
        result = result.Select(x => x.Trim()).ToList();
        result.RemoveAll(string.IsNullOrEmpty);
        return result.ToArray();
    }
    private static bool IsSimpleParameter(string parameter)
    {
        bool isInQuotes = false;
        for (int i = 0; i < parameter.Length; i++)
        {
            if (parameter[i] == '"')
            {
                isInQuotes = !isInQuotes;
            }
            else if (parameter[i] == '(' && !isInQuotes)
            {
                return false;
            }
        }
        return true;
    }
}

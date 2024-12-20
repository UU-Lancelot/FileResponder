using System.IO.Compression;
using UU.Lancelot.FileResponder.Replacers;

namespace UU.Lancelot.FileResponder.PlaceholderProcessing;
public class PlaceholderEvaluator
{
    static ReplacerMain replacerMain = new ReplacerMain();
    public string Evaluate(string placeholder)
    {
        char[] specialChars = { '{', '}', ' ', '\r', '\n', '\t' };
        placeholder = placeholder.Trim(specialChars);

        if (string.IsNullOrEmpty(placeholder))
        {
            return "";
        }

        return ProcessPlaceholder(placeholder);

    }
    private string ProcessPlaceholder(string placeholder)
    {
        placeholder = AddDotToFirstBracket(placeholder);
        //0 - class name | 1 - method name | 2 - parameters | 3 - block
        List<string> parts = SplitIntoComponents(placeholder);
        parts[2] = RemoveOuterBrackets(parts[2]);

        List<string> parameterList = SplitParametr(parts[2]);

        for (int i = 0; i < parameterList.Count; i++)
        {
            if (!IsSimpleParameter(parameterList[i]))
            {
                parameterList[i] = ProcessPlaceholder(parameterList[i]);
            }
        }

        if (string.IsNullOrEmpty(parts[3]))
        {
            return replacerMain.ReplaceValue(parts[0], parts[1], parameterList.ToArray());
        }
        else
        {
            var result = replacerMain.ReplaceBlock(parts[0], parts[1], parameterList.ToArray(), parts[3]);
            return result?.ToString() ?? "";
        }
    }
    private static string AddDotToFirstBracket(string placeholder)
    {
        int indexForDot = placeholder.IndexOf('(');
        placeholder = placeholder.Insert(indexForDot, ".");
        return placeholder;
    }
    private static List<string> SplitIntoComponents(string placeholder)
    {
        List<string> parts = placeholder.Split('.', 3).ToList();
        List<string> paraAndBlock = SeprateParameterAndBlock(parts[2]);
        //parameter
        parts[2] = paraAndBlock[0];
        //block if there is no it will be emtpy string ""
        parts.Add(paraAndBlock[1]);
        return parts;
    }
    private static string RemoveOuterBrackets(string placeholder)
    {
        placeholder = placeholder.Remove(0, 1);
        placeholder = placeholder.Remove(placeholder.LastIndexOf(')'));
        return placeholder;
    }
    private static List<string> SplitParametr(string input)
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
        return result;
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
    private static List<string> SeprateParameterAndBlock(string parameter)
    {
        if (!parameter.Contains("}}"))
        {
            return new List<string> { parameter, "" };
        }

        List<string> result = parameter.Split("}}", 2).ToList();
        result.ForEach(x => x = x.Trim());
        return result;
    }

}

using System;
using System.Collections.Generic;
using UU.Lancelot.FileResponder.Replacers;

namespace UU.Lancelot.FileResponder.PlaceholderProcessing;
public class PlaceholderEvaluator
{
    static ReplacerMain replacerMain = new ReplacerMain();

    public string Evaluate(string placeholder)
    {
        if (string.IsNullOrEmpty(placeholder))
        {
            return "";
        }
        else
        {
            placeholder = placeholder.Trim('{', ' ', '}');
            return ProcessPlaceholder(placeholder);
        }
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
        int indexFirst = placeholder.IndexOf('(');
        placeholder = placeholder.Remove(indexFirst, 1);
        placeholder = placeholder.Insert(indexFirst, ".(");
        return placeholder;
    }
    private static string[] SplitIntoComponents(string placeholder)
    {
        string[] parts = placeholder.Split(new char[] { '.' }, 3, StringSplitOptions.RemoveEmptyEntries);
        return parts;
    }
    private static string RemoveOuterBrackets(string placeholder)
    {
        placeholder = placeholder.Remove(0, 1);
        placeholder = placeholder.Remove(placeholder.Length - 1, 1);
        return placeholder;
    }
    private static bool IsSimpleParameter(string parameter)
    {
        bool isInQuotes = true;
        for (int i = 0; i < parameter.Length; i++)
        {
            if (parameter[i] == '"')
            {
                isInQuotes = !isInQuotes;
            }
            else if (parameter[i] == '(' && isInQuotes)
            {
                return false;
            }
        }
        return true;
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
                result.Add(input.Substring(start, i - start).Trim());
                start = i + 1;
            }
        }

        result.Add(input.Substring(start).Trim());
        result.RemoveAll(string.IsNullOrEmpty);
        return result.ToArray();
    }
    // private string ResolveSimplePlaceholder(string className, string methodName, string[] parameters)
    // {
    //     string result = replacerMain.ReplaceValue(className, methodName, parameters);
    //     return result;
    // }
}

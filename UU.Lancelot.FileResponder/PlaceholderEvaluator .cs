using System;
using System.Collections.Generic;
using UU.Lancelot.FileResponder.Replacers;

namespace UU.Lancelot.FileResponder.PlaceholderProcessing;
public class PlaceholderEvaluator
{
    string tempPlaceholder = "";
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
            tempPlaceholder = placeholder;
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

        parts[2] = string.Join(", ", parameterArray);

        return ResolveSimplePlaceholder(parts);
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

    private static string MergeIntoPlaceholder(string[] parts)
    {
        string result = $"{parts[0]}.{parts[1]}({parts[2]})";
        return result;
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
        bool isInQuotes = true;

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
            else if (input[i] == ',' && depthBracket == 0 && isInQuotes)
            {
                result.Add(input.Substring(start, i - start).Trim());
                start = i + 1;
            }
        }

        result.Add(input.Substring(start).Trim());
        result.RemoveAll(x => string.IsNullOrEmpty(x));
        return result.ToArray();
    }

    private static string RemoveSecondDotAndAddBrackets(string placeholder)
    {
        int index = GetNthIndex(placeholder, '.', 2);
        placeholder = placeholder.Remove(index, 1);
        placeholder = placeholder.Insert(index, "(");
        placeholder = placeholder.Insert(placeholder.Length, ")");
        return placeholder;
    }

    private static int GetNthIndex(string inputString, char targetChar, int occurrence)
    {
        int count = 0;
        for (int i = 0; i < inputString.Length; i++)
        {
            if (inputString[i] == targetChar)
            {
                count++;
                if (count == occurrence)
                {
                    return i;
                }
            }
        }
        return -1;
    }


    private string ResolveSimplePlaceholder(string[] partsOfPlaceholder)
    {
        if (string.IsNullOrEmpty(tempPlaceholder))
        {
            return "";
        }
        string className = partsOfPlaceholder[0];
        string methodName = partsOfPlaceholder[1];
        string[] parameters = partsOfPlaceholder[2].Split(", ");
        
        string result = replacerMain.ReplaceValue(className, methodName, parameters);
        string correctVersionOfPlaceholder = RemoveSecondDotAndAddBrackets(string.Join('.', partsOfPlaceholder));

        int startIndex = tempPlaceholder.IndexOf(correctVersionOfPlaceholder);
        int lengthOfPlaceholder = correctVersionOfPlaceholder.Length;

        tempPlaceholder = tempPlaceholder.Remove(startIndex, lengthOfPlaceholder);
        tempPlaceholder = tempPlaceholder.Insert(startIndex, result);

        return result;

    }
}

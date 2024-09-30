using System;
using System.Collections.Generic;
using UU.Lancelot.FileResponder.Replacers;

namespace UU.Lancelot.FileResponder.PlaceholderEvaluator;
public static class PlaceholderEvaluator
{
    static string pomocnyPlaceholder = "";
    static int aktualniPocetVnoreni = 0;
    static int PotrebnyPocetVnoreni;

    static ReplacerMain replacerMain = new ReplacerMain();

    public static string Evaluate(string placeholder)
    {
        pomocnyPlaceholder = placeholder;
        aktualniPocetVnoreni = 0;
        PotrebnyPocetVnoreni = CalculateNestingLevel(placeholder);

        return ProcessPlaceholder(placeholder);
    }

    private static string ProcessPlaceholder(string placeholder)
    {
        placeholder = AddDotToFirstBracket(placeholder);
        string[] parts = SplitIntoComponents(placeholder);
        parts[2] = RemoveOuterBrackets(parts[2]);

        if (IsSimpleParameter(parts[2]))
        {
            ResolveSimplePlaceholder(parts);
        }
        else
        {
            string[] parameterArray = SplitByCommasOutsideBrackets(parts[2]);
            foreach (string parameter in parameterArray)
            {
                if (!IsSimpleParameter(parameter))
                {
                    ProcessPlaceholder(parameter);
                }
            }
        }

        aktualniPocetVnoreni++;
        if (aktualniPocetVnoreni < PotrebnyPocetVnoreni)
        {
            ProcessPlaceholder(pomocnyPlaceholder);
        }

        return pomocnyPlaceholder;
    }

    private static int CalculateNestingLevel(string placeholder)
    {
        int depthBracket = 0;
        int depthQuotation = 0;

        for (int i = 0; i < placeholder.Length; i++)
        {
            if (placeholder[i] == '"' && depthQuotation == 0)
            {
                depthQuotation++;
            }
            else if (placeholder[i] == '"' && depthQuotation == 1)
            {
                depthQuotation--;
            }
            else if (placeholder[i] == '(' && depthQuotation == 0)
            {
                depthBracket++;
            }
        }
        return depthBracket;
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
        int depthQuotation = 0;
        for (int i = 0; i < parameter.Length; i++)
        {
            if (parameter[i] == '"' && depthQuotation == 0)
            {
                depthQuotation++;
            }
            else if (parameter[i] == '"' && depthQuotation == 1)
            {
                depthQuotation--;
            }
            else if (parameter[i] == '(' && depthQuotation == 0)
            {
                return false;
            }
        }
        return true;
    }

    private static string[] SplitByCommasOutsideBrackets(string input)
    {
        List<string> result = new List<string>();
        int start = 0;
        int depthBracket = 0;
        int depthQuotation = 0;

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
            else if (input[i] == '"' && depthQuotation == 0)
            {
                depthQuotation++;
            }
            else if (input[i] == '"' && depthQuotation == 1)
            {
                depthQuotation--;
            }
            else if (input[i] == ',' && depthBracket == 0 && depthQuotation == 0)
            {
                result.Add(input.Substring(start, i - start).Trim());
                start = i + 1;
            }
        }

        result.Add(input.Substring(start).Trim());
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

    private static int GetNthIndex(string s, char t, int n)
    {
        int count = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == t)
            {
                count++;
                if (count == n)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    private static void ResolveSimplePlaceholder(string[] partsOfPlaceholder)
    {
        string result = replacerMain.ReplaceValue(string.Join('.', partsOfPlaceholder));
        pomocnyPlaceholder = pomocnyPlaceholder.Replace(RemoveSecondDotAndAddBrackets(string.Join('.', partsOfPlaceholder)), result);
    }
}

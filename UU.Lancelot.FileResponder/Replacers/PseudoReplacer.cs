using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;
public class PseudoReplacer : IReplacer
{
    public new string ReplaceValue(string placeholder)
    {
        return placeholder;
    }

    public new IEnumerable<object> ReplaceBlock(string placeholder)
    {
        yield return placeholder;
    }

    protected string[] SplitToMethodAndParameters(string placeholder)
    {
        return placeholder.Split('.', 2, StringSplitOptions.RemoveEmptyEntries)
                          .Select(part => part.Trim())
                          .ToArray();
    }
    protected string[] SplitParameter(string parametr)
    {
        List<string> result = new List<string>();
        int start = 0;
        int depthBracket = 0;
        int depthQuotation = 0;
        char character;

        for (int i = 0; i < parametr.Length; i++)
        {
            character = parametr[i];

            if (character == '"' && depthQuotation == 0)
            {
                depthQuotation++;
            }
            else if (character == '"' && depthQuotation == 1)
            {
                depthQuotation--;
            }
            else if (character == '(' && depthQuotation == 0)
            {
                depthBracket++;
            }
            else if (character == ')' && depthQuotation == 0)
            {
                depthBracket--;
            }
            else if (character == ',' && depthBracket == 0 && depthQuotation == 0)
            {
                result.Add(parametr.Substring(start, i - start).Trim());
                start = i + 1;
            }
        }

        result.Add(parametr.Substring(start).Trim());
        return result.ToArray();
    }
}
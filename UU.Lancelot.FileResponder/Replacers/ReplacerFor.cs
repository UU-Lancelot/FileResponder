using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using UU.Lancelot.FileResponder.FormatIO;
using UU.Lancelot.FileResponder.Interfaces;
using UU.Lancelot.FileResponder.PlaceholderProcessing;

namespace UU.Lancelot.FileResponder.Replacers;

public class ReplacerFor : IBlockReplacer, IReplacer
{
    private readonly Dictionary<string, int> _loopVariables = new();

    public string ReplaceBlock(string className, string methodName, string[] parameters, string block)
    {
        switch (methodName)
        {
            case "Repeat":
                return Repeat(parameters[0], int.Parse(parameters[1]), block);
            case "Current":
                return Current(parameters[0]);
            default:
                throw new NotImplementedException();
        }
    }

    public string ReplaceValue(string className, string methodName, string[] parameters)
    {
        throw new NotImplementedException();
    }

    private string Repeat(string iterationName, int count, string block)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                _loopVariables[iterationName] = i;
                string processedBlock = block.Replace(block, Current(iterationName));
                result.Append(processedBlock);
            }

            _loopVariables.Remove(iterationName);
            return result.ToString();
        }

    private string Current(string variableName)
    {
        string x = _loopVariables.ContainsKey(variableName) ? _loopVariables[variableName].ToString() : string.Empty;
        return x;
    }
}

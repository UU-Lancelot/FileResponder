using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;

public class ReplacerFor : IBlockReplacer, IReplacer
{
    public IEnumerable<object> ReplaceBlock(string className, string methodName, string[] parameters, string block)
    {
        throw new NotImplementedException();
    }

    public string ReplaceValue(string className, string methodName, string[] parameters)
    {
        throw new NotImplementedException();
    }
}
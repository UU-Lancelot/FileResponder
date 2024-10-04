using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;
public abstract class ReplacerBase : IReplacer
{
    public abstract string ReplaceValue(string className, string methodName, string[] parameters);
    public abstract IEnumerable<object> ReplaceBlock(string placeholder);
}
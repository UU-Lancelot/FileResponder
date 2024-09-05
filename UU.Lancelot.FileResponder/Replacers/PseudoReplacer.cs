using UU.Lancelot.FileResponder.Interfaces;

namespace UU.Lancelot.FileResponder.Replacers;
public class PseudoReplacer : IReplacer
{
    public string ReplaceValue(string placeholder)
    {
        return placeholder;
    }

    public IEnumerable<object> ReplaceBlock(string placeholder)
    {
        yield return placeholder;
    }
}
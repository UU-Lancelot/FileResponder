using UU.Lancelot.FileResponder.Interfaces;

public class Replacer : IReplacer
{
    public string ReplaceValue(string placeholder)
    {
        return "Replaced value";
    }

    public IEnumerable<object> ReplaceBlock(string placeholder)
    {
        IEnumerable<object> list = new List<object>();
        return list;
    }
}
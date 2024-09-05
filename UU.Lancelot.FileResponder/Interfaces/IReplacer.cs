namespace UU.Lancelot.FileResponder.Interfaces;
public interface IReplacer
{
    string ReplaceValue(string placeholder);
    IEnumerable<object> ReplaceBlock(string placeholder);
}
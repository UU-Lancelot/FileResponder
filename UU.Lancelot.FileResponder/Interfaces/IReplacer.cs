namespace UU.Lancelot.FileResponder.Interfaces;
public interface IReplacer
{
    IEnumerable<object> ReplaceBlock(string placeholder);
    string ReplaceValue(string className, string methodName, string[] parameters);
}
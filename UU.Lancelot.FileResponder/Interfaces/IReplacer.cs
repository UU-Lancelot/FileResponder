namespace UU.Lancelot.FileResponder.Interfaces;
public interface IReplacer
{
    string ReplaceValue(string className, string methodName, string[] parameters);
}
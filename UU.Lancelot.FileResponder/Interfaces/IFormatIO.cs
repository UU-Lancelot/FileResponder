namespace UU.Lancelot.FileResponder.Interfaces;
public interface IFormatIO
{
    string Format(string fileContent, IReplacer replacer);
}
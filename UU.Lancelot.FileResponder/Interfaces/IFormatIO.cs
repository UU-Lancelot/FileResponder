namespace UU.Lancelot.FileResponder.Interfaces;
public interface IFormatIO
{
    void Format(Stream fileContent, Stream resultContent, IReplacer replacer);
}
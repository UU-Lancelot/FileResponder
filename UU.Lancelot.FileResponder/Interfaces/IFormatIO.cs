namespace UU.Lancelot.FileResponder.Interfaces;
public interface IFormatIO
{   
    //, IReplacer replacer) jsem smazal
    void Format(Stream fileContent, Stream resultContent);
}
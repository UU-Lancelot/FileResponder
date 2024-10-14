namespace UU.Lancelot.FileResponder.Interfaces;

public interface IBlockReplacer
{
    IEnumerable<object> ReplaceBlock(string placeholder);
}
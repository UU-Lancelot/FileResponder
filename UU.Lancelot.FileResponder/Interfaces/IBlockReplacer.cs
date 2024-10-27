namespace UU.Lancelot.FileResponder.Interfaces;

public interface IBlockReplacer
{
    string ReplaceBlock(string className, string methodName, string[] parameters, string block);
}
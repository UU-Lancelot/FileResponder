using UU.Lancelot.FileResponder.Configuration;

namespace UU.Lancelot.FileResponder.Watch;
public record InputFileContext
{
    public required string FilePath { get; set; }
    public required string FileContent { get; set; }
    public required InstanceConfiguration InstanceConfiguration { get; set; }
}

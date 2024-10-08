using UU.Lancelot.FileResponder.Configuration;

namespace UU.Lancelot.FileResponder
{
    class DataStructure
    {
        public string FolderInput { get; set; }
        public string FolderOutput { get; set; }
        public string? TemplatePath { get; set; }
        public string InputString { get; set; }
        public string OutputString { get; set; }

        public DataStructure(InstanceConfiguration instanceConfiguration)
        {
            FolderInput = instanceConfiguration.InputDir;
            FolderOutput = instanceConfiguration.OutputDir;
            TemplatePath = instanceConfiguration.TemplatePath;
        }
    }
}
namespace UU.Lancelot.FileResponder.Configuration;
public class InstanceConfiguration
{
    const string PATH = "appsettings.json";
    public string InputDir { get; set; } = string.Empty;
    public string OutputDir { get; set; } = string.Empty;
    public string TemplatePath { get; set; } = string.Empty;
    public string[] DataStores { get; set; } = Array.Empty<string>();


    public static List<InstanceConfiguration> LoadInstances()
    {
        List<InstanceConfiguration> instances = new();

        if (IsFileEmpty(PATH)) { return instances; }

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(PATH)
            .Build();

        instances = configuration.GetSection("Instances").Get<List<InstanceConfiguration>>() ?? new();
        RemoveInvalidInstances(instances);

        return instances;
    }
    static bool IsFileEmpty(string path)
    {
        return !File.Exists(path) || new FileInfo(path).Length == 0;
    }

    public static void RemoveInvalidInstances(List<InstanceConfiguration>? instances)
    {
        instances?.RemoveAll(x => x.IsInvalid());
    }
    public bool IsInvalid()
    {
        return string.IsNullOrWhiteSpace(InputDir) ||
               string.IsNullOrWhiteSpace(OutputDir) ||
               string.IsNullOrWhiteSpace(TemplatePath);
    }
}

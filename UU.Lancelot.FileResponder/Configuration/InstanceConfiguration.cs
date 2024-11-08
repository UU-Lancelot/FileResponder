namespace UU.Lancelot.FileResponder.Configuration;
public class InstanceConfiguration
{
    const string PATH = "appsettings.json";
    public string? InputDir { get; set; }
    public string? OutputDir { get; set; }
    public string? TemplatePath { get; set; }
    public string[]? dataStores { get; set; }
    public static List<InstanceConfiguration> LoadInstances()
    {
        List<InstanceConfiguration> Instances = new List<InstanceConfiguration>();

        if (!IsFileEmpty(PATH))
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(PATH)
                .Build();

            var settings = configuration.GetSection("Instances").Get<List<InstanceConfiguration>>();

            if (settings != null)
            {
                RemoveInvalidInstances(settings);

                foreach (var setting in settings)
                {
                    Instances.Add(setting);
                }
            }
        }

        return Instances;
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

public class InstanceSettings
{
    const string PATH = "appsettings.json";
    public string? InputDir { get; set; }
    public string? OutputDir { get; set; }
    public string? TemplatePath { get; set; }
    public static List<InstanceSettings>? Instances { get; set; } = new List<InstanceSettings>();

    public static void LoadInstances()
    {
        if (!IsFileEmpty(PATH))
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(PATH)
                .Build();
    
            var settings = configuration.GetSection("Instances").Get<List<InstanceSettings>>();
    
            if (settings != null)
            {
                RemoveInvalidInstances(settings);
    
                foreach (var setting in settings)
                {
                    Instances?.Add(setting);
                }
            }
        }
    }

    static bool IsFileEmpty(string path)
    {
        return !File.Exists(path) || new FileInfo(path).Length == 0;
    }

    public static void RemoveInvalidInstances(List<InstanceSettings>? instances)
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

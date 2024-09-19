public class InstanceSettings
{
    public string? InputDir { get; set; }
    public string? OutputDir { get; set; }
    public string? TemplatePath { get; set; }


    public static List<InstanceSettings>? GetInstances()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var settings = configuration.GetSection("Instances").Get<List<InstanceSettings>>();

        RemoveInvalidInstances(settings);

        return settings;
    }

    public static void RemoveInvalidInstances(List<InstanceSettings>? instances)
    {
        instances?.RemoveAll(x => !x.IsValid());
        //smazat
        if (instances != null)
        {
            foreach (var instance in instances)
            {
                instance.Print();
            }
        }
        else
        {
            Console.WriteLine("No instances found");
        }
        //
    }
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(InputDir) &&
               !string.IsNullOrWhiteSpace(OutputDir) &&
               !string.IsNullOrWhiteSpace(TemplatePath);
    }
    //smazat
    public void Print()
    {
        Console.WriteLine($"InputDir: {InputDir} OutputDir: {OutputDir} TemplatePath: {TemplatePath}");
    }
    //
}

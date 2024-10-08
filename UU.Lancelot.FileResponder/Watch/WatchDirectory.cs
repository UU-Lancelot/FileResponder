using UU.Lancelot.FileResponder.Configuration;

namespace UU.Lancelot.FileResponder.Watch;
class WatchDirectory : IDisposable
{
    private string DirectoryPath;
    private List<string> knownFiles = new List<string>();
    private CancellationTokenSource? cancellationTokenSource;
    public Task? task;
    private InstanceConfiguration instanceConfiguration;
    public event EventHandler<(string, InstanceConfiguration)>? pathFileChangedEventHandler;

    public WatchDirectory(InstanceConfiguration instanceConfiguration)
    {
        this.instanceConfiguration = instanceConfiguration;
        DirectoryPath = instanceConfiguration.InputDir!;
    }
    public void StartWatchingDirectory()
    {
        cancellationTokenSource = new CancellationTokenSource();
        task = WatchFile(cancellationTokenSource.Token);
    }

    private async Task WatchFile(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            List<string> newFiles = SearchFiles();

            foreach (string file in newFiles)
            {
                pathFileChangedEventHandler?.Invoke(this, (file, instanceConfiguration));
            }

            await Task.Delay(1000);
        }
    }
    public List<string> SearchFiles()
    {
        List<string> allFiles = Directory.GetFiles(DirectoryPath).ToList();
        var newFiles = allFiles.Except(knownFiles).ToList();

        knownFiles = allFiles;

        return newFiles;
    }
    public void Dispose()
    {
        if (cancellationTokenSource != null && task != null)
        {
            cancellationTokenSource.Cancel();
            task.Wait();
            cancellationTokenSource.Dispose();
        }
    }
}

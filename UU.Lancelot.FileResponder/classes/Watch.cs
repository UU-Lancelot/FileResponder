namespace UU.Lancelot.FileResponder;
class WatchDirectory : IDisposable
{
    private static readonly string DirectoryPath = @"C:\Users\marek\Desktop\testFolder";
    private List<string> knownFiles = new List<string>();
    private CancellationTokenSource cancellationTokenSource;
    private Task task;

    public event EventHandler<string> pathFileChangedEventHandler;

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
                pathFileChangedEventHandler?.Invoke(null, file);
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
        cancellationTokenSource.Cancel();
        task.Wait();
        cancellationTokenSource.Dispose();
    }
}

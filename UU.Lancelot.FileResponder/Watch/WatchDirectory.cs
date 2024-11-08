using UU.Lancelot.FileResponder.Configuration;

namespace UU.Lancelot.FileResponder.Watch;
class WatchDirectory : IDisposable
{
    private readonly InstanceConfiguration _instanceConfiguration;
    private readonly Action<string> _newFileHandler;
    private List<string> _knownFiles;
    private CancellationTokenSource? _cancellationTokenSource;
    public Task? _task;

    public WatchDirectory(InstanceConfiguration instanceConfiguration, Action<string> newFileHandler)
    {
        _instanceConfiguration = instanceConfiguration;
        _newFileHandler = newFileHandler;
        _knownFiles = new List<string>();
    }

    public void StartWatchingDirectory()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _task = WatchFile(_cancellationTokenSource.Token);
    }

    private async Task WatchFile(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            List<string> newFiles = SearchFiles();

            foreach (string file in newFiles)
            {
                _newFileHandler.Invoke(file);
            }

            await Task.Delay(1000);
        }
    }
    public List<string> SearchFiles()
    {
        List<string> allFiles = Directory.GetFiles(_instanceConfiguration.InputDir).ToList();
        var newFiles = allFiles.Except(_knownFiles).ToList();

        _knownFiles = allFiles;

        return newFiles;
    }

    public void Dispose()
    {
        if (_cancellationTokenSource != null && _task != null)
        {
            _cancellationTokenSource.Cancel();
            _task.Wait();
            _cancellationTokenSource.Dispose();
        }
    }
}

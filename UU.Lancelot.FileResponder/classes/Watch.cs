namespace UU.Lancelot.FileResponder;
public class WatchDirectory : IDisposable
{
    private static readonly string DirectoryPath = @"C:\Users\marek\Desktop\testFolder";
    private static List<string> knownFiles = new List<string>();
    private static CancellationTokenSource cancellationTokenSource;
    private static Task task;

    public WatchDirectory()
    {
        cancellationTokenSource = new CancellationTokenSource();
        task = WatchFile(cancellationTokenSource.Token);
    }

    private static async Task WatchFile(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(1000);
            
            List<string> newFiles = SearchFiles();

            if (newFiles.Count > 0)
            {
                foreach (string file in newFiles)
                {
                    DeleteFile(file);
                }
            }
        }
    }

    public static List<string> SearchFiles()
    {
        List<string> allFiles = Directory.GetFiles(DirectoryPath).ToList();
        var newFiles = allFiles.Except(knownFiles).ToList();

        knownFiles = allFiles; // Update known files with all files in the directory

        return newFiles;
    }

    public static void DeleteFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
    }

    public void Dispose()
    {
        cancellationTokenSource.Cancel();
        task.Wait();

        cancellationTokenSource.Dispose();
    }
}

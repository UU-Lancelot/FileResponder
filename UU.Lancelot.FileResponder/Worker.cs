namespace UU.Lancelot.FileResponder;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation(Jokes.GetJoke());
            }
            await Task.Delay(5000, stoppingToken);
        }
    }
}

public class Jokes
{
    static string[] jokesArray = 
    {
    "Why did the scarecrow win an award? Because he was outstanding in his field.",
    "I'm reading a book on the history of glue. I just can't seem to put it down.",
    "Why don't scientists trust atoms? Because they make up everything.",
    "I told my wife she should embrace her mistakes. She gave me a hug.",
    "I'm reading a book on anti-gravity. It's impossible to put down.",
    "I used to play piano by ear, but now I use my hands."
    };

    public static string GetJoke()
    {
        Random random = new Random();
        int index = random.Next(jokesArray.Length);
        
        return jokesArray[index];
    }
}
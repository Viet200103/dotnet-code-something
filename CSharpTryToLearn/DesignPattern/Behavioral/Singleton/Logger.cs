namespace CSharpTryToLearn.DesignPattern.Behavioral.Singleton;

public sealed class Logger
{
    
    private readonly string _logFilePath;

    private static Object _lock = new Object();
    private static Logger? _instance = null;


    public static Logger GetInstance(string logFilePath)
    {
        lock (_lock)
        {
            if (_instance == null)
            {
                _instance = new Logger(logFilePath);
            }

            return _instance;
        }
    }

    private Logger(string logFilePath)
    {
        _logFilePath = logFilePath;
    }

    public void Log(string message)
    {
        lock (_lock)
        {
            using StreamWriter writer = new StreamWriter(_logFilePath, append: true);
            writer.WriteLine($"{DateTime.Now:u} - {message}");
        }
    }

    public class LoggerTest
    {
        
        public static void Run()
        {
            Logger logger = Logger.GetInstance("app_log.txt");
            
            // Simulate different parts of the app logging
            logger.Log("App started.");
            logger.Log("User logged in.");
            logger.Log("Critical error encountered in module X.");
            
            using StreamReader streamReader = new StreamReader("app_log.txt");
            Console.WriteLine(streamReader.ReadToEnd());
        }
    }
}
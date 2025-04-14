namespace CSharpTryToLearn.DesignPattern.StructuralPattern.Adapter;

/**
 * Scenario: Adapting a Legacy Logging System to a Modern Logging Framework
 */
public class LegacyLogger
{
    public enum LogLevel
    {
        Information,
        Warning,
        Error
    }

    public interface ILogger
    {
        Task LogAsync(LogLevel level, string message, Dictionary<string, string>? metadata = null);
    }

    public class OldLogger
    {
        private bool _isFileOpen;
        private readonly string _filePath;

        public OldLogger(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public void OpenFile()
        {
            if (_isFileOpen)
            {
                throw new InvalidOperationException("File is already open");
            }

            _isFileOpen = true;
            Console.WriteLine($"Legacy: Opened log file {_filePath}");
        }

        public void WriteLog(string text)
        {
            if (!_isFileOpen)
            {
                throw new InvalidOperationException("File is not open");
            }

            // Simulate flaky behavior
            if (Random.Shared.Next(0, 10) == 0)
            {
                throw new IOException("Legacy: Failed to write to file");
            }

            Console.WriteLine($"Legacy: Wrote to {_filePath}: {text}");
        }

        public void CloseFile()
        {
            if (!_isFileOpen)
            {
                throw new InvalidOperationException("File is not open");
            }

            _isFileOpen = false;
            Console.WriteLine($"Legacy: Closed log file {_filePath}");
        }
    }

    public class LoggerAdapter : OldLogger, ILogger
    {
        private readonly object _lock = new object();

        public LoggerAdapter(string filePath) : base(filePath)
        {
        }

        public Task LogAsync(LogLevel level, string message, Dictionary<string, string>? metadata = null)
        {
            if (String.IsNullOrEmpty(message))
            {
                return Task.CompletedTask;
            }

            string logMessage = FormatLogMessage(level, message, metadata);

            return Task.Run(() =>
            {
                lock (_lock)
                {
                    try
                    {
                        OpenFile();
                        WriteLog(logMessage);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine($"Adapter: Failed to log: {ex.Message}");
                        throw;
                    }
                    finally
                    {
                        try
                        {
                            CloseFile();
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }
                }
            });
        }

        private string FormatLogMessage(LogLevel level, string message, Dictionary<string, string>? metadata)
        {
            string levelPrefix = level switch
            {
                LogLevel.Information => "[INFO]",
                LogLevel.Warning => "[WARN]",
                LogLevel.Error => "[ERROR]",
                _ => "[UNKNOWN]"
            };

            string metadataText = metadata != null && metadata.Count > 0
                ? $" {{ {string.Join(", ", metadata.Select(kv => $"{kv.Key}={kv.Value}"))} }}"
                : string.Empty;

            return $"{levelPrefix} {message}{metadataText}";
        }
    }

    public class LoggerProgram
    {
        [Program.RunnableEntry]
        public static void Run()
        {
            LoggerProgram program = new LoggerProgram();
            Task.WhenAll(program.Execute());
            Console.ReadLine();
        }

        private async Task Execute()
        {
            try
            {
                ILogger logger = new LoggerAdapter("log.txt");

                await logger.LogAsync(LogLevel.Information, "User logged in", new Dictionary<string, string>
                {
                    { "UserId", "123" },
                    { "SessionId", "abc456" }
                });

                await logger.LogAsync(LogLevel.Warning, "Payment processing delayed");

                await logger.LogAsync(LogLevel.Error, "Database connection failed", new Dictionary<string, string>
                {
                    { "ErrorCode", "DB001" }
                });

                // Simulate a flaky failure
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        await logger.LogAsync(LogLevel.Information, $"Attempt {i + 1}");
                        break;
                    }
                    catch (IOException)
                    {
                        Console.WriteLine("Retrying due to legacy failure...");
                        await Task.Delay(100);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
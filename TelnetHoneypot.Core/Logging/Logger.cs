namespace TelnetHoneypot.Core.Logging;

public class Logger
{
    private static readonly object LogLock = new();
    private static readonly string LogFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

    public static void Log(LogType logType, string message)
    {
        if (logType == LogType.None)
        {
            return;
        }
        
        lock (LogLock)
        {
            var logFilePath = GetLogFilePath();

            using var writer = new StreamWriter(logFilePath, true);
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            writer.WriteLine($"{timestamp}#{logType.ToString().ToUpper()}: {message}");
        }
    }

    private static string GetLogFilePath()
    {
        Directory.CreateDirectory(LogFolder);
        var logFile = DateTime.Now.ToString("yyyy-MM-dd") + "_telnet_honeypot.log";
        return Path.Combine(LogFolder, logFile);
    }
}
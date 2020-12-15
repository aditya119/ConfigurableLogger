using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LoggerLibrary
{
    public class LoggingService : ILoggingService
    {
        private readonly LogConfigModel _logConfig;
        private readonly ReaderWriterLock _locker;

        public LoggingService(LogConfigModel logConfig)
        {
            _logConfig = logConfig;
            _locker = new ReaderWriterLock();
        }

        private static string GetExceptionString(Exception exception)
        {
            string message = $"[ERROR]: {exception.Message}\n{exception.StackTrace}\n{exception.Data}";
            if (exception.InnerException is not null)
            {
                message = $"{message}\n{exception.InnerException.Message}\n{exception.InnerException.StackTrace}";
            }
            return message;
        }

        private string GetLogFilePath()
        {
            if (Directory.Exists(_logConfig.LogFolder) == false)
            {
                Directory.CreateDirectory(_logConfig.LogFolder);
            }
            string filename = $"{_logConfig.LogFilenamePrefix}_{DateTime.Today:dd-MMM-yyyy}.log";
            return Path.Combine(_logConfig.LogFolder, filename);
        }

        private void Log(string message)
        {
            message = $"{DateTime.Now:dd-MMM-yyyy_HH:mm:ss(zzz)}: {message}";
            if (_logConfig.LogFolder == "@Console")
            {
                Console.WriteLine(message);
                return;
            }
            string logFilePath = GetLogFilePath();
            try
            {
                _locker.AcquireReaderLock(1000);
                using StreamWriter sw = File.Exists(logFilePath) ? File.AppendText(logFilePath) : File.CreateText(logFilePath);
                sw.WriteLine(message);
            }
            finally
            {
                _locker.ReleaseWriterLock();
            }
        }

        private async Task LogAsync(string message)
        {
            message = $"{DateTime.Now:dd-MMM-yyyy_HH:mm:ss(zzz)}: {message}";
            if (_logConfig.LogFolder == "@Console")
            {
                Console.WriteLine(message);
                return;
            }
            string logFilePath = GetLogFilePath();
            try
            {
                _locker.AcquireReaderLock(1000);
                using StreamWriter sw = File.Exists(logFilePath) ? File.AppendText(logFilePath) : File.CreateText(logFilePath);
                await sw.WriteLineAsync(message);
            }
            finally
            {
                _locker.ReleaseWriterLock();
            }
        }

        public void LogDebug(string message)
        {
            if (_logConfig.LogLevel == LogLevel.Debug)
            {
                Log($"[DEBUG]: {message}");
            }
        }

        public async Task LogDebugAsync(string message)
        {
            if (_logConfig.LogLevel == LogLevel.Debug)
            {
                await LogAsync($"[DEBUG]: {message}");
            }
        }

        public void LogInformation(string message)
        {
            LogLevel[] includeLogLevels = new LogLevel[] { LogLevel.Debug, LogLevel.Information };
            if (includeLogLevels.Contains(_logConfig.LogLevel))
            {
                Log($"[INFO]: {message}");
            }
        }

        public async Task LogInformationAsync(string message)
        {
            LogLevel[] includeLogLevels = new LogLevel[] { LogLevel.Debug, LogLevel.Information };
            if (includeLogLevels.Contains(_logConfig.LogLevel))
            {
                await LogAsync($"[INFO]: {message}");
            }
        }

        public void LogError(Exception exception)
        {
            string message = GetExceptionString(exception);
            Log(message);
        }

        public async Task LogErrorAsync(Exception exception)
        {
            string message = GetExceptionString(exception);
            await LogAsync(message);
        }
    }
}

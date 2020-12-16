using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigurableLogger
{
    public partial class LoggingService : ILoggingService
    {
        private readonly LogConfigModel _logConfig;
        private readonly ReaderWriterLock _locker;

        public LoggingService(LogConfigModel logConfig)
        {
            _logConfig = logConfig;
            _locker = new ReaderWriterLock();
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
    }
}

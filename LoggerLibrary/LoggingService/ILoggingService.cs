using System;
using System.Threading.Tasks;

namespace LoggerLibrary
{
    public interface ILoggingService
    {
        void LogDebug(string message);
        Task LogDebugAsync(string message);
        void LogError(Exception exception);
        Task LogErrorAsync(Exception exception);
        void LogInformation(string message);
        Task LogInformationAsync(string message);
    }
}
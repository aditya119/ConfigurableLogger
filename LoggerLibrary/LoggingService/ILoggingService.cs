using System;
using System.Threading.Tasks;

namespace ConfigurableLogger
{
    public interface ILoggingService
    {
        void LogDebug(string message);
        void LogDebug<T>(string message, T model) where T : class;
        Task LogDebugAsync(string message);
        Task LogDebugAsync<T>(string message, T model) where T : class;
        void LogError(Exception exception);
        Task LogErrorAsync(Exception exception);
        void LogInformation(string message);
        Task LogInformationAsync(string message);
    }
}
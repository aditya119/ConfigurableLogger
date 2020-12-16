using System;
using System.Threading.Tasks;

namespace ConfigurableLogger
{
    public partial class LoggingService
    {
        private static string GetExceptionString(Exception exception)
        {
            string message = $"[ERROR]: {exception.Message}\n{exception.StackTrace}\n{exception.Data}";
            if (exception.InnerException is not null)
            {
                message = $"{message}\n{exception.InnerException.Message}\n{exception.InnerException.StackTrace}";
            }
            return message;
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

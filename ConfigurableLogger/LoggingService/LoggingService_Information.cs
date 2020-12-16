using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurableLogger
{
    public partial class LoggingService
    {
        public void LogInformation(string message)
        {
            var includeLogLevels = new string[] { "Debug", "Information" };
            if (includeLogLevels.Contains(_logConfig.LogLevel))
            {
                Log($"[INFO]: {message}");
            }
        }

        public async Task LogInformationAsync(string message)
        {
            var includeLogLevels = new string[] { "Debug", "Information" };
            if (includeLogLevels.Contains(_logConfig.LogLevel))
            {
                await LogAsync($"[INFO]: {message}");
            }
        }
    }
}

using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurableLogger
{
    public partial class LoggingService
    {
        public void LogDebug(string message)
        {
            if (_logConfig.LogLevel == "Debug")
            {
                Log($"[DEBUG]: {message}");
            }
        }

        public async Task LogDebugAsync(string message)
        {
            if (_logConfig.LogLevel == "Debug")
            {
                await LogAsync($"[DEBUG]: {message}");
            }
        }

        public void LogDebug<T>(string message, T model)
        {
            if (_logConfig.LogLevel == "Debug")
            {
                string serializedModel = JsonSerializer.Serialize(model);
                LogDebug($"{message}\n{typeof(T)}: {serializedModel}");
            }
        }

        public async Task LogDebugAsync<T>(string message, T model)
        {
            if (_logConfig.LogLevel == "Debug")
            {
                string serializedModel = JsonSerializer.Serialize(model);
                await LogDebugAsync($"{message}\n{typeof(T)}: {serializedModel}");
            }
        }
    }
}

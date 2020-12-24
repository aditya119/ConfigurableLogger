![.NET](https://github.com/aditya119/ConfigurableLogger/workflows/.NET/badge.svg)
# Configurable Logger

ConfigurableLogger is an easy-to-use, and configurable logger.
It supports both synchronous and asynchronous logging.

---

### Installation

ConfigurableLogger can be installed from Nuget.

---

### Setup

Add `LogConfigModel` as a singleton service to provide required configurations.
- `LogConfigModel.LogLevel` can have value "Error", "Information", or "Debug".
- `LogConfigModel.LogFolder` stores the path to folder where log files will be stored. Use "@Console" to log to console.
- `LogConfigModel.LogFilenamePrefix` stores a prefix for the generated log files. Default is set to "app". Sample log file name `app_16-Dec-2020.log`.

Use `services.AddConfigurableLogger(logConfigModel)` in `Startup.ConfigureServices`.

Inject `ILoggingService` where necessary and log data.

### Usage

#### Methods:
- `void LogDebug(string message)`
- `void LogDebug<T>(string message, T model) where T : class`: Logs a serialization of the passed class along with the message
- `Task LogDebugAsync(string message)`
- `Task LogDebugAsync<T>(string message, T model) where T : class`
- `void LogInformation(string message)`
- `Task LogInformationAsync(string message)`
- `void LogError(Exception exception)`: Logs the details and stack-trace of the exception passed as parameter.
- `Task LogErrorAsync(Exception exception)`
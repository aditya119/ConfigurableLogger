namespace ConfigurableLogger
{
    public class LogConfigModel
    {
        /// <summary>
        /// Choose between "Error", "Information", and "Debug"
        /// </summary>
        public string LogLevel { get; init; }

        /// <summary>
        /// Path to the folder where you want to store logs, use "@Console" to log to console instead
        /// </summary>
        public string LogFolder { get; init; }

        /// <summary>
        /// A prefix for log file name, default will be "app_<dd-MMM-yyyy>.log"
        /// </summary>
        public string LogFilenamePrefix { get; init; } = "app";
    }
}

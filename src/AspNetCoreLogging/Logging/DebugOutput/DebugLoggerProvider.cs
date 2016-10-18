using Microsoft.Extensions.Logging;

namespace AspNetCoreLogging.Logging.DebugOutput
{
    internal class DebugLoggerProvider : ILoggerProvider
    {
        public LogLevel MinimumLevel { get; }

        public DebugLoggerProvider(LogLevel minimumLevel)
        {
            MinimumLevel = minimumLevel;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DebugLogger(MinimumLevel);
        }

        public void Dispose()
        {
        }
    }
}
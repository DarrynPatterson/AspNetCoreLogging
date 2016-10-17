using AspNetCoreLogging.Logging.DebugOutput;
using Microsoft.Extensions.Logging;

namespace AspNetCoreLogging
{
    public static class LoggerFactoryExtensions
    {
        public static ILoggerFactory AddDebug(
            this ILoggerFactory factory,
            LogLevel minimumLevel)
        {
            factory.AddProvider(new DebugLoggerProvider(minimumLevel));
            return factory;
        }
    }
}
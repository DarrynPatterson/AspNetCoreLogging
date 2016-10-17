using AspNetCoreLogging.Logging.AppendBlob;
using AspNetCoreLogging.Logging.DebugOutput;
using Microsoft.Extensions.Logging;

namespace AspNetCoreLogging
{
    public static class LoggerFactoryExtensions
    {
        public static ILoggerFactory AddAppendBlob(
            this ILoggerFactory factory,
            string connectionString,
            LogLevel minimumLevel)
        {
            factory.AddProvider(new AppendBlobLoggerProvider(connectionString, minimumLevel));
            return factory;
        }

        public static ILoggerFactory AddDebug(
            this ILoggerFactory factory,
            LogLevel minimumLevel)
        {
            factory.AddProvider(new DebugLoggerProvider(minimumLevel));
            return factory;
        }
    }
}
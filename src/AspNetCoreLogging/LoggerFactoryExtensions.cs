using AspNetCoreLogging.Logging.AppendBlob;
using AspNetCoreLogging.Logging.DebugOutput;
using AspNetCoreLogging.Logging.MongoCollection;
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

        public static ILoggerFactory AddMongoCollection(
            this ILoggerFactory factory,
            string userName,
            string password,
            string host,
            int port,
            string database,
            string collection,
            LogLevel minimumLevel)
        {
            factory.AddProvider(new MongoCollectionLoggerProvider(userName, password, host, port, database, collection, minimumLevel));
            return factory;
        }
    }
}
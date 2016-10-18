using Microsoft.Extensions.Logging;

namespace AspNetCoreLogging.Logging.AppendBlob
{
    internal class AppendBlobLoggerProvider : ILoggerProvider
    {
        public LogLevel MinimumLevel { get; }

        public string StorageAccountConnectionString { get; }

        public AppendBlobLoggerProvider(string storageAccountConnectionString, LogLevel minimumLevel)
        {
            StorageAccountConnectionString = storageAccountConnectionString;
            MinimumLevel = minimumLevel;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new AppendBlobLogger(StorageAccountConnectionString, MinimumLevel, categoryName);
        }

        public void Dispose()
        {
        }
    }
}
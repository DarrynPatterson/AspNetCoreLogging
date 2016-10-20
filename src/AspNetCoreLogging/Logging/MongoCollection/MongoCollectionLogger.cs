using System;
using System.Threading.Tasks;
using AspNetCoreLogging.Logging.MongoCollection.Repository;
using Microsoft.Extensions.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace AspNetCoreLogging.Logging.MongoCollection
{
    internal class MongoCollectionLogger : ILogger
    {
        #region Properties

        private MongoRepository MongoRepository { get; }

        private LogLevel MinimumLevel { get; }

        private string CategoryName { get; }

        #endregion

        #region Constructor

        public MongoCollectionLogger(string userName, string password, string host, int port, string database, string collection, LogLevel minimumLevel, string categoryName)
        {
            MinimumLevel = minimumLevel;
            CategoryName = categoryName;

            var config = new MongoConfig(userName, password, host, port, database, collection);
            MongoRepository = new MongoRepository(config);
        }

        #endregion

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= MinimumLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var task = Task.Factory.StartNew(async () =>
            {
                var date = DateTime.UtcNow.ToString("o");
                var ex = exception != null ? exception.ToString().Replace(Environment.NewLine, " ") : string.Empty;
                var entry = $"[{date}]\t{logLevel.ToString()}\t{CategoryName}\t{state.ToString()}\t{ex}" + Environment.NewLine;

                try
                {
                    await MongoRepository.Insert(entry);
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }
    }
}
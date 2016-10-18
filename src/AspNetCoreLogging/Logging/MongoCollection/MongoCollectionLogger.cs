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
                var entry = String.Format("[{0}]\t{1}\t{2}\t{3}\t{4}" + Environment.NewLine, DateTime.UtcNow.ToString("o"), logLevel.ToString(), CategoryName, state.ToString(), exception != null ? exception.ToString().Replace(Environment.NewLine, " ") : String.Empty);

                try
                {
                    await MongoRepository.Insert(new string[] { entry });
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }
    }
}
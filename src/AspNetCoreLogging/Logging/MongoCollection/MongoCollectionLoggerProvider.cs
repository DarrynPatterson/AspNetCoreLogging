using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AspNetCoreLogging.Logging.MongoCollection
{
    internal class MongoCollectionLoggerProvider : ILoggerProvider
    {
        private string UserName { get; }

        private string Password { get; }

        private string Host { get; }

        private int Port { get; }

        private string Database { get; }

        private string Collection { get; }

        private LogLevel MinimumLevel { get; }

        public MongoCollectionLoggerProvider(string userName, string password, string host, int port, string database, string collection, LogLevel minimumLevel)
        {
            UserName = userName;
            Password = password;
            Host = host;
            Port = port;
            Database = database;
            Collection = collection;
            MinimumLevel = minimumLevel;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new MongoCollectionLogger(UserName, Password, Host, Port, Database, Collection, MinimumLevel, categoryName);
        }

        public void Dispose()
        {
        }
    }
}
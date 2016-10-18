namespace AspNetCoreLogging.Logging.MongoCollection.Repository
{
    public class MongoConfig
    {
        public string UserName { get; }

        public string Password { get; }

        public string Host { get; }

        public int Port { get; }

        public string Database { get; }

        public string Collection { get; }

        public MongoConfig(string userName, string password, string host, int port, string database, string collection)
        {
            UserName = userName;
            Password = password;
            Host = host;
            Port = port;
            Database = database;
            Collection = collection;
        }
    }
}
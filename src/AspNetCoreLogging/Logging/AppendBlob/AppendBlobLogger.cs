using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace AspNetCoreLogging.Logging.AppendBlob
{
    internal class AppendBlobLogger : ILogger
    {
        private CloudAppendBlob appendBlob;

        public CloudAppendBlob AppendBlob
        {
            get
            {
                if (appendBlob == null || appendBlob.Name != AppendBlobName)
                {
                    appendBlob = Container.GetAppendBlobReference(AppendBlobName);
                }

                return appendBlob;
            }
        }

        private string AppendBlobName
        {
            get
            {
                return string.Format("{0}{1}", DateTime.UtcNow.ToString("yyyy-MM-dd/yyyy-MM-dd-HH"), ".log");
            }
        }

        public LogLevel MinimumLevel { get; }

        public CloudBlobContainer Container { get; }

        public string CategoryName { get; }

        public AppendBlobLogger(string connectionString, LogLevel minimumLevel, string categoryName)
        {
            MinimumLevel = minimumLevel;
            CategoryName = categoryName;

            try
            {
                var cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
                var client = cloudStorageAccount.CreateCloudBlobClient();

                Container = client.GetContainerReference("logs");
                Container.CreateIfNotExists();
            }
            catch (Exception)
            {
                throw;
            }
        }

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

            var task = Task.Factory.StartNew(() =>
            {
                var date = DateTime.UtcNow.ToString("o");
                var ex = exception != null ? exception.ToString().Replace(Environment.NewLine, " ") : string.Empty;
                var entry = $"[{date}]\t{logLevel.ToString()}\t{CategoryName}\t{state.ToString()}\t{ex}" + Environment.NewLine;
                
                if (!AppendBlob.Exists())
                {
                    AppendBlob.CreateOrReplace();
                }

                try
                {
                    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(entry)))
                    {
                        AppendBlob.AppendBlock(stream);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }
    }
}
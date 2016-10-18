using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCoreLogging.TestClient
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment environment)
        {
            if (string.IsNullOrWhiteSpace(environment.EnvironmentName))
            {
                throw new ArgumentNullException(nameof(environment.EnvironmentName), "The application's environment name cannot be null or empty.");
            }

            Configuration = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("config.json", optional: false)
                .AddJsonFile($"config.{environment.EnvironmentName}.json", optional: false)
                .AddEnvironmentVariables()
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddDebug(LogLevel.Debug);

            loggerFactory.AddMongoCollection(
                Configuration["Logging:MongoUserName"],
                Configuration["Logging:MongoPassword"],
                Configuration["Logging:MongoHost"],
                int.Parse(Configuration["Logging:MongoPort"]),
                Configuration["Logging:MongoDatabase"],
                Configuration["Logging:MongoCollection"],
                LogLevel.Debug);

            var logger = loggerFactory.CreateLogger<Startup>();
            logger.LogInformation("Log Information");
        }
    }
}
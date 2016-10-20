# AspNetCoreLogging

The [AspNetCoreLogging package](https://www.nuget.org/packages/AspNetCoreLogging/) is an ASP.NET logging solution capable of logging application messages to a Mongo Collection, Azure Blob Storage or the Debug Window.

[![NuGet](https://img.shields.io/nuget/v/AspNetCoreLogging.svg?maxAge=259200)](https://www.nuget.org/packages/AspNetCoreLogging/) 

**NuGet install:**

Install-Package AspNetCoreLogging

Take a look at the 'TestClient' project for a working example of a logger implementation.

**Startup.cs code:**

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
        loggerFactory.AddDebug(LogLevel.Debug);
		
		// NB: Update the Mongo connection details in the config.development.json
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
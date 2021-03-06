﻿using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace AspNetCoreLogging.Logging.DebugOutput
{
    internal class DebugLogger : ILogger
    {
        public LogLevel MinimumLevel { get; }

        public DebugLogger(LogLevel minimumLevel)
        {
            MinimumLevel = minimumLevel;
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

            var date = DateTime.UtcNow.ToString("o");
            var entry = $"[{date}] {logLevel.ToString()} {state.ToString()} {exception?.ToString()}";

            Debug.WriteLine(entry);
        }
    }
}
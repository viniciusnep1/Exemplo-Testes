using Microsoft.Extensions.Logging;
using System;

namespace web.Logger
{
    public static class AppLoggerExtensions
    {
        public static ILoggerFactory AddContext(this ILoggerFactory factory,
            Func<string, LogLevel, bool> filter = null, string connectionString = null)
        {
            using(var logger = new AppLoggerProvider(filter, connectionString))
            {
                factory.AddProvider(logger);
                return factory;
            }
        }

        public static ILoggerFactory AddContext(this ILoggerFactory factory, LogLevel minLevel, string connectionString)
        {
            return AddContext(factory, (_, logLevel) => logLevel >= minLevel, connectionString);
        }
    }
}

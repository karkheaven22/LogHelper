using LogHelper.Logger.EventEnricherExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;

namespace LogHelper.Logger.Extensions
{
    public static class LoggerExtensions
    {
        public static ILoggingBuilder AddLogger(this ILoggingBuilder factory, Serilog.ILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            factory.AddProvider(new SerilogLoggerProvider(logger, false));
            return factory;
        }

        public static IHostBuilder UseLogHelper(this IHostBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.UseSerilog((context, configuration) =>
            {
                var loggingEnabled = context.Configuration.GetValue("LoggingEnabled", true);

                if (!loggingEnabled)
                {
                    configuration.Filter.ByExcluding(_ => true);
                    return;
                }

                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.WithThreadId()
                    .Enrich.WithApplication()
                    .Enrich.FromLogContext();
            });

            return builder;
        }
    }
}
using LogHelper.Logger.EventEnricherExtensions;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace LogHelper.Logger
{
    internal sealed class AppLogger
    {
        private readonly IConfiguration _configuration;
        private readonly bool _isConsoleEnabled;
        private readonly bool _isLoggingEnabled;
        public ILogger Logger { get; }

        public static AppLogger Instance { get; } = new AppLogger();

        public AppLogger() : this(DefaultConfiguration()) { }

        public AppLogger(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _isLoggingEnabled = _configuration.GetValue("LoggingEnabled", true);
            _isConsoleEnabled = _configuration.GetValue("LoggingEnabled", true);
            Logger = CreateLogger();
        }

        private static IConfiguration DefaultConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment}.json", true, true)
                .Build();
        }

        public ILogger CreateLogger()
        {
            if (!_isLoggingEnabled)
            {
                return new LoggerConfiguration()
                    .Filter.ByExcluding(_ => true)
                    .CreateLogger();
            }

            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .Enrich.WithApplication();

            if (_isConsoleEnabled)
            {
                loggerConfiguration.WriteTo.Console(theme: SystemConsoleTheme.Literate);
            }

            return loggerConfiguration
                .ReadFrom.Configuration(_configuration)
                .CreateLogger();
        }
    }
}
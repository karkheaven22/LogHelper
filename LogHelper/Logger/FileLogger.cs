using LogHelper.Logger.SeriLogExtensions;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Settings.Configuration;
using Serilog.Sinks.SystemConsole.Themes;

namespace LogHelper.Logger
{
    internal sealed class FileLogger
    {
        private bool IsConsoleEnabled;
        private bool IsLoggingEnabled;
        private IConfiguration? Configuration;
        private ILogger _logger = null!;
        public ILogger Logger => _logger;

        public static FileLogger Instance { get; } = new FileLogger();

        public FileLogger() : this(null) { }

        public FileLogger(IConfiguration? configuration)
        {
            Initialize(configuration);
        }

        private void Initialize(IConfiguration? configuration)
        {
            Configuration = configuration ?? DefaultConfiguration();
            IsLoggingEnabled = Convert.ToBoolean(Configuration.GetSection("LoggingEnabled").Value);
            IsConsoleEnabled = Convert.ToBoolean(Configuration.GetSection("ConsoleEnabled").Value);
            _logger = CreateLogger();
        }

        private static IConfiguration DefaultConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment}.json", true, true)
                .Build();
        }

        public ILogger CreateLogger()
        {
            var options = new ConfigurationReaderOptions { SectionName = "Filelog" };

            return new LoggerConfiguration()
                    .Filter.ByExcluding(_ => !IsLoggingEnabled)
                    .MinimumLevel.Debug()
                    .Enrich.FromLogContext()
                    .Enrich.WithThreadId()
                    .Enrich.WithCustom()
                    .WriteTo.Conditional(evt => IsConsoleEnabled, wt => wt.Console(theme: SystemConsoleTheme.Literate))
                    .ReadFrom.Configuration(Configuration!, options)
                    .CreateLogger();
        }
    }
}
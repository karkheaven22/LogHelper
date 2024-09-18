using Serilog.Configuration;
using Serilog;


namespace LogHelper.Logger.SeriLogExtensions
{
    public static class ConfigurationExtensions
    {
        public static LoggerConfiguration WithThreadId(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<ThreadIdEnricher>();
        }

        public static LoggerConfiguration WithCustom(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<CustomEnricher>();
        }
    }
}

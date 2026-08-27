using Serilog.Configuration;
using Serilog;

namespace LogHelper.Logger.EventEnricherExtensions;

public static class EnricherExtensions
{
    public static LoggerConfiguration WithThreadId(this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        return enrichmentConfiguration == null
            ? throw new ArgumentNullException(nameof(enrichmentConfiguration))
            : enrichmentConfiguration.With<ThreadIdEnricher>();
    }

    public static LoggerConfiguration WithApplication(this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        return enrichmentConfiguration == null
            ? throw new ArgumentNullException(nameof(enrichmentConfiguration))
            : enrichmentConfiguration.With<ApplicationEnricher>();
    }

    public static LoggerConfiguration WithAddress(this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        return enrichmentConfiguration == null
            ? throw new ArgumentNullException(nameof(enrichmentConfiguration))
            : enrichmentConfiguration.With<AddressEnricher>();
    }
}

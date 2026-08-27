using Serilog.Core;
using Serilog.Events;
using System.Reflection;

namespace LogHelper.Logger.EventEnricherExtensions;

internal class ApplicationEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var applicationAssembly = Assembly.GetEntryAssembly();
        var name = applicationAssembly?.GetName().Name ?? string.Empty;
        var version = applicationAssembly?.GetName().Version ?? new Version("0.0.0");
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("appName", name));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("appVersion", version));
    }
}

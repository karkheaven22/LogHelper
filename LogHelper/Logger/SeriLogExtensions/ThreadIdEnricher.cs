using Serilog.Core;
using Serilog.Events;

namespace LogHelper.Logger.SeriLogExtensions
{
    internal class ThreadIdEnricher : ILogEventEnricher
    {
        public const string ThreadIdPropertyName = "ThreadId";
        private static readonly ThreadLocal<LogEventProperty> _lastValue = new ThreadLocal<LogEventProperty>();
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var threadId = Environment.CurrentManagedThreadId;

            var last = _lastValue.Value;
            if (last == null || (int)(new ScalarValue(last.Value)?.Value ?? 0)  != threadId)
            {
                // If the thread ID has changed, create a new property
                last = new LogEventProperty(ThreadIdPropertyName, new ScalarValue(threadId));
                _lastValue.Value = last; // Update the thread-local cache with the new value
            }

            logEvent.AddPropertyIfAbsent(last);
        }
    }
}
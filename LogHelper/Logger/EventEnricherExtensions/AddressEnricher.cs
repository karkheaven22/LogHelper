using Serilog.Core;
using Serilog.Events;
using System.Net;
using System.Net.Sockets;

namespace LogHelper.Logger.EventEnricherExtensions
{
    internal class AddressEnricher : ILogEventEnricher
    {
        public static string GetHostName()
        {
            return Dns.GetHostName();
        }

        public static string GetIPAddress(bool getIPV6 = false)
        {
            try
            {
                var ipAddresses = Dns.GetHostAddresses(GetHostName()).ToList();

                if (!ipAddresses.Any())
                {
                    return string.Empty;
                }
                else
                {
                    ipAddresses = [.. ipAddresses.Where(ip => !IPAddress.IsLoopback(ip))];
                }

                foreach (var ipAddress in ipAddresses)
                {
                    if (getIPV6 && ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        return ipAddress.ToString();
                    }
                    else if (!getIPV6 && ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ipAddress.ToString();
                    }
                }
            }
            catch (Exception)
            {
                // Noncompliant
            }

            return string.Empty;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("host", GetIPAddress(false)));
        }
    }
}
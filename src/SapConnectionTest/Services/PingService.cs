using System;
using System.Net.NetworkInformation;

internal static class PingService
{
    internal static void Ping(string address)
    {
        Ping ping = new();
        try
        {
            PingReply pingReply = ping.Send(address);
            if (pingReply.Status != IPStatus.Success)
            {
                throw new PingException("Icmp Ping KO");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}

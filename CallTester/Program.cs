using Serilog;
using Serilog.Core;
using SipClient;
using SipClient.Extensions;
using SipClient.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CallTester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Define logger
            Console.WriteLine("=== STARTING ===");
            // *** Define serilog logger ***
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({Name:l}) {Message}{NewLine}{Exception}")
                .MinimumLevel.Debug()
                .CreateLogger();
 
            // *** Building SIP Client ***
            ISipClient sipClient = new SipBuilder()
                .SetUser("300")
                .SetDefaultAllowedMethods()
                .SetRegistrarUri(new byte[] { 10, 0, 0, 150 }) // local home 10.0.0.150
                //.SetRegistrarUri(new Uri("sip:hotfoon.com"))
                .SetClientUri(new byte[] { 10, 0, 0, 126 }, 5060)
                .UseDefaultSipClient();

            await sipClient.RegisterAsync();

            while(true)
            {
                Thread.Sleep(Timeout.Infinite);
            }
        }
    }
}

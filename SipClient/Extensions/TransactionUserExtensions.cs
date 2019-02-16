using Javor.SipSerializer;
using Javor.SipSerializer.HeaderFields;
using Javor.SipSerializer.Schemes;
using SipClient.Models;
using System;
using System.Threading.Tasks;

namespace SipClient.Extensions
{
    /// <summary>
    ///     Transaction user extensions.
    /// </summary>
    public static class TransactionUserExtensions
    {
        public static async Task RegisterAsync(this ISipClient sipClient)
        {
            SipDialogue sd = sipClient.GetNewDialogue(RequestMethods.Register);
            await sd.StartDialogueFlow();
        }

        public static async Task<bool> SipUdpPingAsync(this ISipClient sipClient, string targetHost, int port)
        {
            SipDialogue sd = sipClient.GetNewDialogue(RequestMethods.Options);
            await sd.StartDialogueFlow();

            return true;
        }
    }
}
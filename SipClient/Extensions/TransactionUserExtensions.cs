using Javor.SipSerializer;
using Javor.SipSerializer.HeaderFields;
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
            SipDialogue sd = sipClient.GetNewDialogue();
            Identification from = new Identification(sipClient.Account.RegistrarUri, null, sd.DialogueId);

            await sd.SendSipRequestAsync(RequestMethods.Register, from);
        }

        public static async Task SendSipRequestAsync(this SipDialogue sipDialogue, string requestMethod, Identification from, Identification to = null)
        {
            foreach (Guid uriId in sipDialogue.DestinationURIs.Keys)
            {
                SipRequestMessage request = new SipRequestMessage(
                    requestMethod,
                    sipDialogue.DestinationURIs[uriId].ToString());

                request.Headers.From = from.ToString();
                request.Headers.Contact = from.ToString();
                request.Headers.CallId = uriId.ToString();

                if (to != null)
                    request.Headers.To = to.ToString();
                else
                    request.Headers.To = from.ToString();

                await sipDialogue.TransactionLayer.SendSipRequest(request);
            }
        }
    }
}
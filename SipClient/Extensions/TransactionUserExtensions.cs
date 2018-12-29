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
            Identification to = new Identification(sipClient.Account.RegistrarUri, null);

            await sd.SendSipRequestAsync(RequestMethods.Register, to);
        }

        public static async Task SendSipRequestAsync(this SipDialogue sipDialogue, string requestMethod, Identification to, Identification from = null)
        {
            foreach (Guid uriId in sipDialogue.DestinationURIs.Keys)
            {
                SipRequestMessage request = new SipRequestMessage(
                    requestMethod,
                    sipDialogue.DestinationURIs[uriId].ToString());

                request.Headers.To = to;
                request.Headers.Contact = to.ToString();
                request.Headers.CallId = uriId.ToString();

                if (from != null)
                    request.Headers.From = from;
                else
                    request.Headers.From = new Identification(to.Uri, sipDialogue.DialogueId);

                await sipDialogue.TransactionLayer.SendSipRequest(request);
            }
        }
    }
}
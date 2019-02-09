using Javor.SipSerializer;
using Javor.SipSerializer.HeaderFields;
using SipClient.Instances;
using SipClient.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SipClient
{
    public interface ISipClient
    {
        string SipVersion { get; }

        IEnumerable<string> AllowedMethods { get; set; }
        SipClientAccount Account { get; }

        /// <summary>
        ///     Transaction layer responsible for sip message sending.
        /// </summary>
        ISipTransactionLayer TransactionLayer { get; }

        /// <summary>
        ///     Creates and return new dialogue.
        /// </summary>
        /// <returns>Sip dialogue.</returns>
        SipDialogue GetNewDialogue();
    }
}
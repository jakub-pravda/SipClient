using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Javor.SipSerializer;
using SipClient.Logging;
using SipClient.Models;

namespace SipClient.Instances
{
    public class TransactionUser : ISipTransactionUser
    {
        public List<Uri> DestinationUris { get; private set; }
            = new List<Uri>();
    }

    public interface ISipTransactionUser
    {
    }

    /// <summary>
    ///     Trasnaction layer interface.
    /// </summary>
    public interface ISipTransactionLayer
    {
        /// <summary>
        ///     Send sip request message as new transaction.
        /// </summary>
        /// <param name="message">Sip message.</param>
        /// <param name="destination">User agent server uri.</param>
        /// <param name="destinationPort">User agent server port.</param>
        /// <param name="waitForResponse"></param>
        /// <returns></returns>
        Task<bool> SendSipRequest(SipRequestMessage request, bool waitForResponse = false);
    }
}

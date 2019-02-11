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
    /// <summary>
    ///     Trasnaction layer interface.
    /// </summary>
    public interface ISipTransactionLayer
    {
        /// <summary>
        ///     Transaction complete event raiser.
        /// </summary>
        event TransactionCompleteHandler TransactionComplete;
        /// <summary>
        ///     Send sip request message as new transaction.
        /// </summary>
        /// <param name="message">Sip message.</param>
        /// <param name="destination">User agent server uri.</param>
        /// <param name="destinationPort">User agent server port.</param>
        /// <param name="waitForResponse"></param>
        /// <returns></returns>
        Task<bool> SendSipRequestAsync(SipRequestMessage request, bool waitForResponse = false);

        /// <summary>
        ///     Start with listening on the designated ip address:port
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        void StartListening(string ipAddress, int port);
        void StopListening();
    }
}

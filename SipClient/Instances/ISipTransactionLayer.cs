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
        Task<bool> SendSipRequestAsync(SipRequest request, bool waitForResponse = false);

        Task<bool> ProcessSipMessageAsync(string sipMessage);
        
        /// <summary>
        ///     Process s new sip request.
        /// </summary>
        /// <param name="sipMessage">Incoming sip message</param>
        /// <returns>Sip response for incoming sip request</returns>
        Task<SipResponse> ProcessSipRequestAsync(string sipMessage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sipMessage"></param>
        /// <returns></returns>
        Task ProcessSipResponseAsync(string sipMessage);
    }
}

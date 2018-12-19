using Javor.SipSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SipClient.Models
{
    /// <summary>
    ///     Sip transaction.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        ///     Transaction ID.
        /// </summary>
        public string TransactionId { get; }

        /// <summary>
        ///     Provisional response list (1xx responses).
        /// </summary>
        public List<SipReponseMessage> ProvisionalResponses { get; } = new List<SipReponseMessage>(4);

        /// <summary>
        ///     Initial transaction request.
        /// </summary>
        public SipRequestMessage InitialRequest { get; set; }

        /// <summary>
        ///     Final response message (non 1xx message).
        /// </summary>
        public List<SipReponseMessage> FinalResponses { get; } = new List<SipReponseMessage>();

        /// <summary>
        ///     Initialize new transaction.
        /// </summary>
        /// <param name="transactionId">Transaction id.</param>
        public Transaction(SipRequestMessage initialRequest)
        {
            if (initialRequest == null) throw new ArgumentNullException("Invalid initial SIP request message.");

            InitialRequest = initialRequest;

            int viaCount = initialRequest.Headers.Via.Count;
            TransactionId = initialRequest.Headers.Via.Last().Branch;
        }
    }
}

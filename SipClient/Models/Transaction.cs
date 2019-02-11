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

        public TransactionStatus Status { get; private set; }

        /// <summary>
        ///     Provisional response list (1xx responses).
        /// </summary>
        public List<SipResponseMessage> ProvisionalResponses { get; } = new List<SipResponseMessage>(4);

        /// <summary>
        ///     Initial transaction request.
        /// </summary>
        public SipRequestMessage InitialRequest { get; }

        /// <summary>
        ///     Final response message (non 1xx message).
        /// </summary>
        public SipResponseMessage FinalResponse { get; private set; }

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
            Status = TransactionStatus.INIT;
        }

        public bool SetResponse(SipResponseMessage sipResponse)
        {
            if (sipResponse.StatusLine.StatusCode.StartsWith("1"))
            {
                // provisional response
                ProvisionalResponses.Add(sipResponse);

                if (Status == TransactionStatus.INIT)
                {
                    Status = TransactionStatus.PROVISIONAL;
                }
                else if (Status != TransactionStatus.PROVISIONAL)
                {
                    throw new ArgumentException("Invalid state of transaction status!");
                }

                return false;
            }

            FinalResponse = sipResponse;
            Status = TransactionStatus.COMPLETE;
            return true;
        }

        public enum TransactionStatus
        {
            INIT,
            PROVISIONAL,
            COMPLETE
        }
    }
}

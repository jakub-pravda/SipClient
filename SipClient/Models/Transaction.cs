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
        public List<SipResponse> ProvisionalResponses { get; } = new List<SipResponse>(4);

        /// <summary>
        ///     Initial transaction request.
        /// </summary>
        public LazySipMessage Request { get; }

        /// <summary>
        ///     Final response message (non 1xx message).
        /// </summary>
        public SipResponse FinalResponse { get; private set; }

        /// <summary>
        ///     Initial new transaction
        /// </summary>
        /// <param name="sipRequest">Sip request message</param>
        public Transaction(LazySipMessage sipRequest)
        {
            if (sipRequest == null) throw new ArgumentNullException("Sip request cann't be null");

            Request = sipRequest;

            TransactionId = initialRequest.Headers.Via.Last().Branch; // need last branch id!
            Status = TransactionStatus.INIT;
        }

        public bool SetResponse(SipResponse sipResponse)
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

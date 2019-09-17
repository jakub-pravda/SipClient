using Javor.SipSerializer;
using Javor.SipSerializer.HeaderFields;
using Javor.SipSerializer.Helpers;
using Javor.SipSerializer.Schemes;
using SipClient.Logging;
using SipClient.Models;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Javor.SipSerializer.SipMessage;

namespace SipClient.Instances
{
    /// <summary>
    ///     Default transaction agent implementation
    /// </summary>
    public class TransactionAgent : IDisposable
    {
        private readonly ILog _logger = LogProvider.GetCurrentClassLogger();
        private bool _disposed = false;
        private CancellationTokenSource _cancellationTokenSource;
        private Func<SipRequest, SipResponse> _requestHandler;

        /// <summary>
        ///     Transaction complete event.
        /// </summary>
        public event TransactionCompleteHandler TransactionComplete;

        ConcurrentDictionary<string, Transaction> Transactions { get; }
            = new ConcurrentDictionary<string, Transaction>();

        /// <summary>
        ///     Initialize new transaction layer.
        /// </summary>
        public TransactionAgent(Func<SipRequest, SipResponse> requestHandler)
        {
            if (requestHandler == null) throw new ArgumentNullException("Request handler cann't be null");

            _requestHandler = requestHandler;
        }

        bool ProcessSipMessageAsync(string sipMessage) 
        {
            LazySipMessage lazySip = new LazySipMessage(sipMessage);
            
            if(lazySip.SipType == SipMessageType.Request)
            {
                // set new transaction
                
            } 
            else if (lazySip.SipType == SipMessageType.Response)
            {
                throw new NotImplementedException();
            }
            else
            {
                _logger.Warn($"Uninidifiable sip message type. Sip message: {sipMessage}");
            }
        }

        /// <summary>
        ///     Send sip request as new transaction.
        /// </summary>
        /// <param name="request">Sip message.</param>
        /// <param name="destination">User agent server uri.</param>
        /// <param name="destinationPort">User agent server port.</param>
        /// <returns></returns>
        public async Task<bool> SendSipRequestAsync(SipRequest request)
        {
            if (request == null) throw new ArgumentNullException("Request message cann't be null.");


            SetNewTransaction(request);

            string strRequest = request.ToString();

            _logger.Debug(string.Format("Sending SIP message\n{0}", strRequest));

            return false; // TODO
        }

        protected virtual void OnTransactionComplete(Transaction transaction)
        {
            TransactionCompleteHandler del = TransactionComplete as TransactionCompleteHandler;
            if (del != null)
            {
                del(this, new TransactionCompleteEventArgs(transaction));
            }
        }

        private void SetNewTransaction(SipRequest initialRequest)
        {
            if (_via == null) throw new ArgumentNullException("VIA SIP header cann't be null. Listening agent was not properly started.");

            // generate new VIA message
            initialRequest.Headers.Via.Add(_via);

            // generate new transaction
            Transaction tr = new Transaction(initialRequest);

            bool result = Transactions.TryAdd(_via.Branch, tr);
        }

        /// <summary>
        ///     Dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            // if disposing is true, method has been called directly by user code
            // if disposing is false, method has been called by the runtime from insade the finalizer

            if (_disposed)
                return;

            if (disposing)
            {
                StopListening();
            }

            _disposed = true;
        }

        ~TransactionAgent()
        {
            Dispose(false);
        }
    }

    class BufferSegment
    {
        public byte[] Buffer { get; set; }
        public int Count { get; set; }

        public int Remaining
        {
            get
            {
                return Buffer.Length - Count;
            }
        }
    }

    public class TransactionCompleteEventArgs
    {
        public Transaction Transaction { get; private set; }

        public TransactionCompleteEventArgs(Transaction transaction)
        {
            Transaction = transaction;
        }
    }
    }
}
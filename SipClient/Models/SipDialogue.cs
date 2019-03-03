using SipClient.Logging;
using Javor.SipSerializer;
using Javor.SipSerializer.HeaderFields;
using Javor.SipSerializer.Helpers;
using Javor.SipSerializer.Schemes;
using SipClient.Instances;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SipClient.Models
{
    /// <summary>
    ///     Sip dialogue.
    /// </summary>
    public class SipDialogue
    {
        private int _lastSeqNumber = 0;
        private static readonly object _locker = new object();
        private ISipTransactionLayer _transactionLayer;
        private ISipResponseHandler _responseHandler;
        private string _initRequest;
        private readonly ILog _logger = LogProvider.GetCurrentClassLogger();

        /// <summary>
        ///     Unique dialogue identificator.
        /// </summary>
        public string DialogueId { get; set; }

        /// <summary>
        ///     Dialogue state.
        /// </summary>
        public DialogueState State { get; private set; }
             = DialogueState.CREATED;

        /// <summary>
        ///     FROM dialogue identification.
        /// </summary>
        public Identification From { get; set; }

        /// <summary>
        ///     TO dialogue identification.
        /// </summary>
        public Identification To { get; set; }

        /// <summary>
        ///     Sip request destination.
        /// </summary>
        public SipUri DestinationUri { get; private set; }

        private SipDialogue()
        {
            _logger.Debug("New dialogue created.");
        }

        public SipDialogue(Action<ISipResponseHandler> sipResponseHandler)
            : this()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Initialize new sip dialogue.
        /// </summary>
        /// <param name="initRequestMethod"></param>
        /// <param name="requestUri"></param>
        /// <param name="from"></param>
        /// <param name="transactionLayer"></param>
        public SipDialogue(string initRequestMethod, SipUri requestUri, Identification from, ISipTransactionLayer transactionLayer)
            : this()
        {
            From = from;
            To = new Identification(from.Uri, null);
            DestinationUri = requestUri;
            
            _transactionLayer = transactionLayer;
            _transactionLayer.TransactionComplete += PrivateTransactionAgent_TransactionComplete;

            DialogueId = DialogueHelpers.GenerateCallId();
            _responseHandler = new DefaultResponseHandler();
            _initRequest = initRequestMethod; // Temporary solution, must be handled by "dialogue flow"

            // TODO create default action with init request method
        }

        /// <summary>
        ///     Starts the dialogue flow.
        /// </summary>
        public async Task StartDialogueFlow()
        {
            _logger.Debug("Starting new dialogue flow.");

            SipRequestMessage toSend = PrivateGetSipRequest(_initRequest);

            // create unique tag
            toSend.Headers.From.Tag = DialogueHelpers.GenerateIdentificationTag();

            await _transactionLayer.SendSipRequestAsync(toSend);
        }

        private SipRequestMessage PrivateGetSipRequest(string requestMethod)
        {
            SipRequestMessage request = new SipRequestMessage(requestMethod, DestinationUri);
            request.Headers.From = From;
            request.Headers.To = To;
            request.Headers.Contact = From.ToString();
            request.Headers.CallId = DialogueId;

            return request;
        }

        private void PrivateTransactionAgent_TransactionComplete(object sender, TransactionCompleteEventArgs e)
        {
            if (e.Transaction.FinalResponse.Headers.CallId == DialogueId)
            {
                _responseHandler.ProcessSipMessage(e.Transaction.FinalResponse);
            }
        }

        private int GetSeqNumber()
        {
            lock (_locker)
            {
                return _lastSeqNumber++;
            }
        }

        /// <summary>
        ///     Dialogue processing state.
        /// </summary>
        public enum DialogueState
        {
            CREATED,
            PROCESSING,
            COMPLETE
        }
    }
}

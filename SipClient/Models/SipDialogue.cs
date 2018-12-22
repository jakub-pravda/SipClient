using Javor.SipSerializer;
using Javor.SipSerializer.HeaderFields;
using Javor.SipSerializer.Schemes;
using SipClient.Instances;
using System;
using System.Collections.Generic;
using System.Text;

namespace SipClient.Models
{
    /// <summary>
    ///     Sip dialogue.
    /// </summary>
    public class SipDialogue
    {
        private int _lastSeqNumber = 0;
        private static readonly object _locker = new object();

        /// <summary>
        ///     Unique dialogue identificator.
        /// </summary>
        public string DialogueId { get; set; }

        /// <summary>
        ///     List of destination uris (each destination uri has own unique id).
        /// </summary>
        public Dictionary<Guid, SipUri> DestinationURIs { get; set; }
            = new Dictionary<Guid, SipUri>();

        /// <summary>
        ///     Transaction layer for this sip dialogue.
        /// </summary>
        public ISipTransactionLayer TransactionLayer { get; }

        /// <summary>
        ///     Initialize new sip dialogue.
        /// </summary>
        /// <param name="dialogueId">Unique dialogue Id..</param>
        public SipDialogue(string dialogueId, ISipTransactionLayer transactionLayer, SipUri destinationUri)
        {
            TransactionLayer = transactionLayer;
            DialogueId = dialogueId;
            AddNewDestinationUri(destinationUri);
        }

        public void AddNewDestinationUri(SipUri destinationUri)
        {
            if (destinationUri == null) throw new ArgumentNullException("Invalid destination URI.");
            
            DestinationURIs.Add(Guid.NewGuid(), destinationUri);
        }

        public void AddNewDestiantionUris(IEnumerable<SipUri> destinationUris)
        {
            foreach (SipUri destinationUri in destinationUris)
            {
                AddNewDestinationUri(destinationUri);
            }
        }

        private int GetSeqNumber()
        {
            lock (_locker)
            {
                return _lastSeqNumber++;
            }
        }
    }
}

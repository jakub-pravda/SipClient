using Javor.SipSerializer.Attributes;
using Javor.SipSerializer.Exceptions;
using Javor.SipSerializer.HeaderFields;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Javor.SipSerializer
{
    /// <summary>
    ///     Sip request message.
    /// </summary>
    public class SipRequestMessage : SipMessage
    {
        private SipRequestMessage()
        {
            Type = SipMessageType.Request;
        }

        public SipRequestMessage(string requestType, Uri uri, int sequenceNumber)
            : this()
        {
            RequestLine = new RequestLine(requestType, uri);

            // define headers
            Headers.CSeq = new CSeq(sequenceNumber, requestType);
        }

        public SipRequestMessage(RequestLine requestLine)
            : this()
        {
            RequestLine = requestLine;
        }
        
        /// <summary>
        ///     Request line.
        /// </summary>
        public RequestLine RequestLine { get; }
    }
}
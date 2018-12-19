using Javor.SipSerializer.Attributes;
using Javor.SipSerializer.Exceptions;
using Javor.SipSerializer.HeaderFields;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Javor.SipSerializer
{
    /// <summary>
    ///     Sip request message.
    /// </summary>
    public class SipRequestMessage : SipMessage
    {
        /// <summary>
        ///     Request line.
        /// </summary>
        public RequestLine RequestLine { get; }

        private SipRequestMessage()
            : base()
        {
            Type = SipMessageType.Request;
        }

        public SipRequestMessage(string requestType, Uri uri, int cSeq = 0)
            : this()
        {
            RequestLine = new RequestLine(requestType, uri);
            Headers.CSeq = new CSeq(cSeq, RequestLine.Method);
        }

        public SipRequestMessage(string requestType, string uri, int cSeq = 0)
            : this()
        {
            RequestLine = new RequestLine(requestType, uri);
            Headers.CSeq = new CSeq(cSeq, RequestLine.Method);
        }

        public SipRequestMessage(RequestLine requestLine, int cSeq = 0)
            : this()
        {
            RequestLine = requestLine;
            Headers.CSeq = new CSeq(cSeq, RequestLine.Method);
        }

        /// <summary>
        ///     Convert SIP request message into the string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(RequestLine.ToString());
            sb.Append(base.ToString());

            return sb.ToString();
        }
    }
}
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
        {
            Type = SipMessageType.Request;
        }

        public SipRequestMessage(string requestType, Uri uri)
            : this()
        {
            RequestLine = new RequestLine(requestType, uri);
        }

        public SipRequestMessage(string requestType, string uri)
            : this()
        {
            RequestLine = new RequestLine(requestType, uri);
        }

        public SipRequestMessage(RequestLine requestLine)
            : this()
        {
            RequestLine = requestLine;
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
using Javor.SipSerializer.Attributes;
using Javor.SipSerializer.Exceptions;
using Javor.SipSerializer.HeaderFields;
using Javor.SipSerializer.Helpers;
using Javor.SipSerializer.Schemes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Javor.SipSerializer
{
    /// <summary>
    ///     Sip request message
    /// </summary>
    public class SipRequest : BaseSip
    {
        /// <summary>
        ///     Request line
        /// </summary>
        public RequestLine RequestLine { get; private set; }

        private SipRequest()
        {
            Type = SipMessageType.Request;
        }

        /// <summary>
        ///     Initialize new sip request
        /// </summary>
        /// <param name="requestType">Request type</param>
        /// <param name="uri">Sip uri</param>
        /// <param name="cSeq">Cseq number</param>
        public SipRequest(string requestType, SipUri uri, int cSeq = 0)
            : this()
        {
            RequestLine = new RequestLine(requestType, uri, Constants.SipVersion);
            Headers.CSeq = new CSeq(cSeq, RequestLine.Method);
        }

        /// <summary>
        ///     Initialize new sip request
        /// </summary>
        /// <param name="requestType">Request type</param>
        /// <param name="uri">Sip uri</param>
        /// <param name="cSeq">Cseq number</param>
        public SipRequest(string requestType, string uri, int cSeq = 0)
            : this()
        {
            RequestLine = new RequestLine(requestType, new SipUri(uri), Constants.SipVersion);
            Headers.CSeq = new CSeq(cSeq, RequestLine.Method);
        }

        /// <summary>
        ///     Initialize new sip request
        /// </summary>
        /// <param name="requestLine">Request line</param>
        /// <param name="cSeq">CSeq number</param>
        public SipRequest(RequestLine requestLine, int cSeq = 0)
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
            sb.Append(RequestLine.ToString());
            sb.Append(base.ToString());

            return sb.ToString();
        }
    }
}
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
    ///     Sip request message.
    /// </summary>
    public class SipRequest : SipMessage
    {
        /// <summary>
        ///     Request line.
        /// </summary>
        public RequestLine RequestLine { get; private set; }

        private SipRequest()
            : base()
        {
            Type = SipMessageType.Request;
        }

        public SipRequest(string requestType, SipUri uri, int cSeq = 0)
            : this()
        {
            RequestLine = new RequestLine(requestType, uri, Constants.SipVersion);
            Headers.CSeq = new CSeq(cSeq, RequestLine.Method);
        }

        public SipRequest(string requestType, string uri, int cSeq = 0)
            : this()
        {
            RequestLine = new RequestLine(requestType, new SipUri(uri), Constants.SipVersion);
            Headers.CSeq = new CSeq(cSeq, RequestLine.Method);
        }

        public SipRequest(RequestLine requestLine, int cSeq = 0)
            : this()
        {
            RequestLine = requestLine;
            Headers.CSeq = new CSeq(cSeq, RequestLine.Method);
        }

        /// <summary>
        ///     Deserialize and set request line.
        /// </summary>
        /// <param name="requestLine">Request line in string form.</param>
        public void DeserializeAndSetRequestLine(string requestLine)
        {
            var parsed = requestLine.Trim().Split(ABNF.SP);

            if (!ParsingHelpers.IsRequestLine(parsed, out string err))
            {
                throw new SipParsingException(err);
            }
            
            RequestLine = new RequestLine(parsed[0], new SipUri(parsed[1]), Constants.SipVersion);
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
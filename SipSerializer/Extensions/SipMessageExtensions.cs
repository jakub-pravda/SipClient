using Javor.SipSerializer.Exceptions;
using Javor.SipSerializer.HeaderFields;
using System;

namespace Javor.SipSerializer.Extensions
{
    public static class SipMessageExtension
    {
        public static StandardHeaders CreateHeaders(this SipMessage sipMessage)
        {
            throw new NotImplementedException();

            //StandardHeaders headers = new StandardHeaders()
            //{
            //    CSeq = CSeq.Parse(sipMessage.GetHeaderValue(HeaderName.Cseq)[0]),
            //    From = Identification.Parse(sipMessage.GetHeaderValue(HeaderName.From)[0]),
            //    To = Identification.Parse(sipMessage.GetHeaderValue(HeaderName.To)[0]),
            //    CallId = sipMessage.GetHeaderValue(HeaderName.CallId)[0],
            //    MaxForwards = sipMessage.GetHeaderValue(HeaderName.MaxForwards)[0]
            //};
        }

        public static SipRequest CreateRequest(this SipMessage sipMessage)
        {
            if (sipMessage.GetMessageType() != SipMessageType.Request)
                throw new SipParsingException();

            throw new NotImplementedException();
        }

        public static SipResponse CreateResponse(this SipMessage sipMessage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Initialize new SIP response.
        /// </summary>
        /// <param name="sipRequest">SIP request from which SIP response will be created.</param>
        /// <param name="statusCode">SIP response status code.</param>
        /// <returns>SIP response message.</returns>
        public static SipResponse GetResponse(this SipRequest sipRequest, StatusCode statusCode)
        {
            StatusLine sl = new StatusLine(statusCode);
            SipResponse response = new SipResponse(sl);

            // required response headers
            response.Headers.From = sipRequest.Headers.From;
            response.Headers.CallId = sipRequest.Headers.CallId;
            response.Headers.CSeq = sipRequest.Headers.CSeq;
            response.Headers.Via = sipRequest.Headers.Via;
            response.Headers.To = sipRequest.Headers.To;
            response.Headers.ContentLength = 0;

            return response;
        }
    }
}

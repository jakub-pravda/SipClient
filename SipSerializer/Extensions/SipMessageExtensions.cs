using Javor.SipSerializer.Exceptions;
using Javor.SipSerializer.HeaderFields;
using System;

namespace Javor.SipSerializer.Extensions
{
    public static class SipMessageExtension
    {
        
        public static void FillSipHeaders(this BaseSip baseSip, SipMessage sipMessage)
        {
            throw new NotImplementedException();

            // baseSip.CSeq = CSeq.Parse(sipMessage.GetHeaderValue(HeaderName.Cseq)[0]);
            // baseSip.From = Identification.Parse(sipMessage.GetHeaderValue(HeaderName.From)[0]);
            // baseSip.To = Identification.Parse(sipMessage.GetHeaderValue(HeaderName.To)[0]);
            // baseSip.CallId = sipMessage.GetHeaderValue(HeaderName.CallId)[0];
            // baseSip.MaxForwards = sipMessage.GetHeaderValue(HeaderName.MaxForwards)[0];
            // baseSip.Via = Via.ParseMultiple(sipMessage.GetHeaderValue(HeaderName.Via));

        }

        public static SipRequest CreateRequest(this SipMessage sipMessage)
        {
            if (sipMessage.GetMessageType() != SipMessageType.Request)
                throw new SipParsingException("Must be sip request");

            throw new NotImplementedException();
        }

        public static SipResponse CreateResponse(this SipMessage sipMessage)
        {
            if (sipMessage.GetMessageType() != SipMessageType.Response)
                throw new SipParsingException("Must be sip response");

            throw new NotImplementedException();
        }

        /// <summary>
        ///     Initialize new SIP response.
        /// </summary>
        /// <param name="request">SIP request from which SIP response will be created.</param>
        /// <param name="statusCode">SIP response status code.</param>
        /// <returns>SIP response message.</returns>
        public static SipResponse GetResponse(this SipRequest request, StatusCode statusCode)
        {
            StatusLine sl = new StatusLine(statusCode);
            SipResponse response = new SipResponse(sl);

            // required response headers
            response.AddFromHeader((Identification)request.GetHeaderOrDefault(HeaderName.From));
            response.AddCallIdHeader((string)request.GetHeaderOrDefault(HeaderName.CallId));
            response.AddCseqHeader((CSeq)request.GetHeaderOrDefault(HeaderName.Cseq));
            response.AddToHeader((Identification)request.GetHeaderOrDefault(HeaderName.To));

            foreach (Via item in request.GetAllHeadersOrDefault(HeaderName.Via))
                response.AddViaHeader(item);

            response.AddContentLengthHeader((int)request.GetHeaderOrDefault(HeaderName.ContentLength));
    

            return response;
        }

        public static void AddCseqHeader(this BaseSip sip, CSeq cSeq)
        {   
            sip.AddHeader(HeaderName.Cseq, cSeq);
        }
        

        public static void AddFromHeader(this BaseSip sip, Identification from)
        {   
            sip.AddHeader(HeaderName.From, from);
        }

        public static void AddCallIdHeader(this BaseSip sip, string callId)
        {   
            sip.AddHeader(HeaderName.CallId, callId);
        }

        public static void AddViaHeader(this BaseSip sip, Via via)
        {   
            sip.AddHeader(HeaderName.Via, via);
        }

        public static void AddToHeader(this BaseSip sip, Identification to)
        {   
            sip.AddHeader(HeaderName.To, to);
        }

        public static void AddContentLengthHeader(this BaseSip sip, int contentLength)
        {   
            sip.AddHeader(HeaderName.ContentLength, contentLength);
        }
    }
}

using Javor.SipSerializer.Exceptions;
using Javor.SipSerializer.HeaderFields;
using System;

namespace Javor.SipSerializer.Extensions
{
    public static class SipMessageExtension
    {
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

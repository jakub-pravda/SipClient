using System;
using System.Collections.Generic;
using System.Text;

namespace Javor.SipSerializer.Extensions
{
    public static class SipRequestExtensions
    {
        /// <summary>
        ///     Initialize new SIP response.
        /// </summary>
        /// <param name="sipRequest">SIP request from which SIP response will be created.</param>
        /// <param name="statusCode">SIP response status code.</param>
        /// <returns>SIP response message.</returns>
        public static SipReponseMessage CreateResponse(this SipRequestMessage sipRequest, StatusCode statusCode)
        {
            StatusLine sl = new StatusLine(statusCode);
            SipReponseMessage response = new SipReponseMessage(sl);

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

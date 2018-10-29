using Javor.SipSerializer.Extensions;
using Javor.SipSerializer.Models;
using System;

namespace Javor.SipSerializer.Helpers
{
    public static class SipRequestHelpers
    {
        /// <summary>
        ///     Create new sip request from encoded sip message.
        /// </summary>
        /// <param name="SipMessage">Raw sip message.</param>
        /// <returns>Sip request message.</returns>
        public static SipRequestMessage CreateSipRequest(string SipMessage)
        {
            // separate message into the pieces (headers, bodies, ...)
            string[] messageParts
                = SipMessage.Split(new string[] { ABNF.CRLF + ABNF.CRLF }, StringSplitOptions.None);

            // get request line + headers
            string[] reqLineAndHeaders
                = messageParts[0].Split(new string[] { ABNF.CRLF }, 2, StringSplitOptions.None);

            SipRequestMessage sipRequest = new SipRequestMessage(new RequestLine(reqLineAndHeaders[0]));
            sipRequest.AddHeaders(reqLineAndHeaders[1]);

            return sipRequest;
        }

        public static SipRequestMessage CreateSipRegister(SipRegisterOptions sipRegisterOptions)
        {
            if (!sipRegisterOptions.IsValid())
                throw new MissingFieldException("Model validation failed.");

            // create REGISTER message
            SipRequestMessage register = new SipRequestMessage(
                RequestMethods.Register,
                sipRegisterOptions.RequestUri,
                sipRegisterOptions.SequenceNumber);

            // fill REGISTER mandatory fields
            register.Headers.To = sipRegisterOptions.To;
            register.Headers.From = sipRegisterOptions.From;
            register.Headers.CallId = sipRegisterOptions.CallId;
            register.Headers.Contact = sipRegisterOptions.Contact;

            return register;
        }
    }
}

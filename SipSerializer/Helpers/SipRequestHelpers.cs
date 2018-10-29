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

        public static SipRequestMessage CreateSipRegister(SipRegisterOptions sipRegisterMandatoryFields)
        {
            // create REGISTER message
            SipRequestMessage register = new SipRequestMessage(
                RequestMethods.Register,
                sipRegisterMandatoryFields.RequestUri,
                sipRegisterMandatoryFields.SequenceNumber);

            // fill REGISTER mandatory fields
            register.Headers.To = sipRegisterMandatoryFields.To;
            register.Headers.From = sipRegisterMandatoryFields.From;
            register.Headers.CallId = sipRegisterMandatoryFields.CallId;
            register.Headers.Contact = sipRegisterMandatoryFields.Contact;

            // TODO check register message validity or register options validity

            return register;
        }
    }
}

using Javor.SipSerializer.HeaderFields;
using System;

namespace Javor.SipSerializer
{
    /// <summary>
    ///     Lazy SIP message
    /// </summary>
    public class LazySipMessage
    {
        private RawSipMessage _sipMessage;

        /// <summary>
        ///     Instantiate new lazy SIP message
        /// </summary>
        /// <param name="sipMessage">Sip message</param>
        public LazySipMessage(string sipMessage)
        {
            if (string.IsNullOrEmpty(sipMessage)) throw new ArgumentNullException("Sip message cann`t be null");

            _sipMessage = new RawSipMessage(sipMessage);
        }

        // Standard headers
        public string getFromHeaderValue() => _sipMessage.GetHeaderValue(HeaderFieldsNames.From);
        public string getToHeaderValue() => _sipMessage.GetHeaderValue(HeaderFieldsNames.To);
        public string getCallIdHeaderValue() => _sipMessage.GetHeaderValue(HeaderFieldsNames.CallId);
        public string getViaHeaderValue() => _sipMessage.GetHeaderValue(HeaderFieldsNames.Via);
        public string getContentLengthHeaderValue() => _sipMessage.GetHeaderValue(HeaderFieldsNames.ContentLength);
    }

}
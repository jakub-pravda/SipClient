using Javor.SipSerializer.HeaderFields;
using System;
using System.Linq;

namespace Javor.SipSerializer
{
    /// <summary>
    ///     Lazy SIP message
    /// </summary>
    public class LazySipMessage
    {
        private SipMessage _sipMessage;

        /// <summary>
        ///     Sip message type
        /// </summary>
        public SipMessageType SipType 
        { 
            get
            {
                if (_sipType == SipMessageType.Unknown)
                {
                    _sipType = _sipMessage.GetMessageType();
                }
                return _sipType;
            } 
            private set
            {
                _sipType = value;
            }
        }
        private SipMessageType _sipType = SipMessageType.Unknown;

        /// <summary>
        ///     Instantiate new lazy SIP message
        /// </summary>
        /// <param name="sipMessage">Sip message</param>
        public LazySipMessage(string sipMessage)
        {
            if (string.IsNullOrEmpty(sipMessage)) throw new ArgumentNullException("Sip message cann`t be null");

            _sipMessage = new SipMessage(sipMessage);
        }

        // Standard headers
        public string getFromHeaderValue() => _sipMessage.GetHeaderValue(HeaderName.From).First();
        public string getToHeaderValue() => _sipMessage.GetHeaderValue(HeaderName.To).First();
        public string getCallIdHeaderValue() => _sipMessage.GetHeaderValue(HeaderName.CallId).First();
        public string getViaHeaderValue() => _sipMessage.GetHeaderValue(HeaderName.Via).First();
        public string getContentLengthHeaderValue() => _sipMessage.GetHeaderValue(HeaderName.ContentLength).First();
    }

}
using Javor.SipSerializer.Bodies;
using Javor.SipSerializer.HeaderFields;
using System.Collections.Generic;
using System.Text;

namespace Javor.SipSerializer
{
    // TODO create compact form
    // TODO supported and require header
    /// <summary>
    ///     Base SIP message model.
    /// </summary>
    public abstract class BaseSip
    {
        /// <summary>
        ///     Sip message unique id.
        /// </summary>
        public string Id
        {
            get
            {
                return Headers.CallId;
            }
        }

        /// <summary>
        ///     Sip message headers.
        /// </summary>
        public StandardHeaders Headers { get; set; }
            = new StandardHeaders();

        /// <summary>
        ///     Sip message bodies.
        /// </summary>
        public ICollection<ISipBody> Bodies { get; set; }

        /// <summary>
        ///     Sip message type.
        /// </summary>
        public SipMessageType Type { get; protected set; }

        /// <summary>
        ///     Checks if sip message is valid.
        ///      Returns relevant validity code.
        /// </summary>
        public SipMessageValidityCode IsValid
        {
            get
            {
                return CheckValidity();
            }
        }

        private SipMessageValidityCode CheckValidity()
        {
            if (Headers.ContentLength > 0)
            {
                // TODO check content length validity
                // check if sip message contains whole content
            }

            return SipMessageValidityCode.Valid;
        }

        /// <summary>
        ///     Convert SIP message into the string.
        /// </summary>
        /// <returns>String representation of the SIP message.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Headers.ToString());
            sb.Append(ABNF.CRLF);

            return sb.ToString();
        }
    }
}
using Javor.SipSerializer.Attributes;
using Javor.SipSerializer.Bodies;
using Javor.SipSerializer.Extensions;
using Javor.SipSerializer.HeaderFields;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private ICollection<SipHeader<object>> _headers;

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
                throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Add sip header
        /// </summary>
        /// <param name="name">Header name</param>
        /// <param name="value">Header value</param>
        public void AddHeader(string name, object value)
        {
            _headers.Add(new SipHeader<object>(name, value));
        }

        public object GetHeaderOrDefault(string headerName)
        {
            return _headers.FirstOrDefault(h => h.Name == headerName).Value;
        }

        public object[] GetAllHeadersOrDefault(string headerName)
        {
            return _headers
                .Where(h => h.Name == headerName)
                .Select(h => h.Value)
                .ToArray();
        }

        /// <summary>
        ///     Convert SIP message into the string.
        /// </summary>
        /// <returns>String representation of the SIP message.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            // headers to string
            foreach (SipHeader<object> item in _headers)
            {
                sb.Append(item.ToString());
            }

            sb.Append(ABNF.CRLF);

            return sb.ToString();
        }
    }
}
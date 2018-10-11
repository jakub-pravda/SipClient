using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading;
using Javor.SipSerializer;
using Javor.SipSerializer.Attributes;
using Javor.SipSerializer.Bodies;
using Javor.SipSerializer.Exceptions;
using Javor.SipSerializer.HeaderFields;

namespace Javor.SipSerializer
{
    // TODO create compact form
    // TODO supported and require header
    /// <summary>
    ///     Base SIP message model.
    /// </summary>
    public abstract class SipMessage
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

        /// <summary>
        ///     Sip message type.
        /// </summary>
        public enum SipMessageType
        {
            /// <summary>
            ///     Unknown type of the sip message.
            /// </summary>
            Unknown,
            /// <summary>
            ///     Sip request message.
            /// </summary>
            Request,
            /// <summary>
            ///     Sip response message.
            /// </summary>
            Response
        }

        /// <summary>
        ///     Ass new sip headers to the sip message.
        /// </summary>
        /// <param name="headerLines">Full header lines in a raw form.</param>
        public void AddHeaders(string headerLines)
        {
            string[] headers
                = headerLines.Split(new string[] { ABNF.CRLF }, StringSplitOptions.None);

            AddHeaders(headers);
        }

        /// <summary>
        ///     Add new sip headers to the sip message.
        /// </summary>
        /// <param name="headers">Full header lines.</param>
        public void AddHeaders(string[] headerLines)
        {
            foreach (string header in headerLines)
            {
                AddHeader(header);
            }
        }

        /// <summary>
        ///     Add new sip header to the message.
        /// </summary>
        /// <param name="headerLine">Full header line.</param>
        public void AddHeader(string headerLine)
        {
            if (string.IsNullOrEmpty(headerLine))
                throw new ArgumentNullException("Header line cann't be null.");

            string[] header = headerLine.Split(new[] { ABNF.HCOLON }, 2, StringSplitOptions.None);
            if (header.Length == 2) // header name, header content
            {
                AddHeader(header[0], header[1]);
            }
            else
            {
                throw new SipParsingException($"Invalid header line: {headerLine}.");
            }
        }

        /// <summary>
        ///     Add new sip header to the message.
        /// </summary>
        /// <param name="headerName">Header name.</param>
        /// <param name="headerContent">Header content.</param>
        public void AddHeader(string headerName, string headerContent)
        {
            if (string.IsNullOrEmpty(headerName))
                throw new ArgumentNullException("Header name cann't be null.");
            if (string.IsNullOrEmpty(headerContent))
                throw new ArgumentNullException("Header content cann't be null.");

            PropertyInfo[] properties = typeof(StandardHeaders)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                HeaderNameAttribute propertyAttribute =
                    (HeaderNameAttribute)property.GetCustomAttribute(typeof(HeaderNameAttribute));

                if (string.Equals(headerName, propertyAttribute?.GetHeaderFullName(),
                        StringComparison.InvariantCultureIgnoreCase)
                    || string.Equals(headerName, propertyAttribute?.GetHeaderCompactFormName(),
                        StringComparison.InvariantCultureIgnoreCase))
                {
                    property.SetValue(Headers, headerContent, BindingFlags.Default, new SipBinder(), null, null);
                    break;
                }
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

            return sb.ToString();
        }
    }
}
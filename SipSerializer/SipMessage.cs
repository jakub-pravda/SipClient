using Javor.SipSerializer;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Javor.SipSerializer
{
    /// <summary>
    ///     Sip message
    /// </summary>
    public class SipMessage
    {
        /// <summary>
        ///     Original sip message
        /// </summary>
        public string Raw { get; private set; }

        /// <summary>
        ///     Initialize new sip message
        /// </summary>
        /// <param name="sipMessage">Original message</param>
        public SipMessage(string sipMessage)
        {
            if (string.IsNullOrEmpty(sipMessage)) throw new ArgumentNullException("Cannt be null or empty");

            Raw = sipMessage;
        }

        /// <summary>
        ///     Get header value
        /// </summary>
        /// <param name="headerName">Header name</param>
        /// <returns>Return header value. Only first value is returned when there are multiple headers with the same name</returns>
        public SipHeader GetHeaderValue(string headerName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Get all available sip headers
        /// </summary>
        /// <returns>Returns all sip headers</returns>
        public IEnumerable<SipHeader> GetSipHeaders()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Get sip message type
        /// </summary>
        /// <returns>Message type (Request, Response, Unknown)</returns>
        public SipMessageType GetMessageType()
        {
            ReadOnlySpan<char> sipMessage = Raw.AsSpan();
            ReadOnlySpan<char> statusLine = sipMessage.Slice(0, sipMessage.IndexOf(ABNF.CRLF.AsSpan()));

            if (statusLine.EndsWith(Constants.SipVersion.AsSpan()))
            {
                return SipMessageType.Request;
            }
            else if (statusLine.StartsWith(Constants.SipVersion.AsSpan()))
            {
                return SipMessageType.Response;
            }

            return SipMessageType.Unknown;
        }

        private string[] FindHeaderValue(ReadOnlySpan<char> sipMessage, string headerName)
        {
            Span<char> upperSipmessage = stackalloc char[sipMessage.Length];
            sipMessage.ToUpperInvariant(upperSipmessage);

            ReadOnlySpan<char> headerSpan = headerName.ToUpper().AsSpan();
            int headerIndex = upperSipmessage.IndexOf(headerSpan);

            if (headerIndex < 0)
                return new string[] { null }; // default string with null

            List<int> indexes = new List<int>(); // odd - start indexes; even - end indexes

            int delimiterPosition = -1;
            bool delimiterFound = false;
            for (int i = headerIndex; i < sipMessage.Length; i++)
            {
                // find delimiter
                if (!delimiterFound && sipMessage[i] == ':')
                {
                    delimiterPosition = i;
                    delimiterFound = true;

                    if (upperSipmessage.Slice(headerIndex, delimiterPosition - headerIndex).IndexOf(headerSpan) == -1)
                        break; // different than wanted header, same header must be in one row
                }

                // find end of the line
                if (sipMessage[i] == '\r' && sipMessage[i + 1] == '\n')
                {
                    indexes.Add(delimiterPosition + 1); // start index
                    indexes.Add(i); // end index

                    delimiterFound = false;
                    headerIndex = i++ + 1; // i++ - skip \n char and set header index to the next char after
                }
            }

            // get all values based on detected indexes
            string[] values = new string[indexes.Count / 2];
            int j = 0;
            for (int i = 0; i < indexes.Count; i += 2)
            {
                values[j++] = sipMessage.Slice(indexes[i], indexes[i + 1] - indexes[i]).ToString();
            }

            return values;
        }
    }
}

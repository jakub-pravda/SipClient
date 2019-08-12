using Javor.SipSerializer;
using System;

namespace Javor.SipSerializer
{
    public class SipMessage
    {
        /// <summary>
        ///     Original sip message
        /// </summary>
        public string Raw { get; private set; }

        public SipMessage(string sipMessage)
        {
            if (string.IsNullOrEmpty(sipMessage)) throw new ArgumentNullException("Cannt be null or empty");

            Raw = sipMessage;
        }

        public string GetHeaderValue(string headerName, StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
        {
            return FindHeaderValue(Raw.AsSpan(), headerName, comparisonType).ToString();
        }

        /// <summary>
        ///     Get sip message type
        /// </summary>
        /// <returns></returns>
        public SipMessageType GetMessageType()
        {
            ReadOnlySpan<char> sipMessage = Raw.AsSpan();
            ReadOnlySpan<char> statusLine = sipMessage.Slice(0, sipMessage.IndexOf(ABNF.CRLF.AsSpan()));

            if (statusLine.EndsWith(Constants.SipVersion.AsSpan()))
            {
                return SipSerializer.SipMessageType.Request;
            }
            else if (statusLine.StartsWith(Constants.SipVersion.AsSpan()))
            {
                return SipSerializer.SipMessageType.Response;
            }

            return SipSerializer.SipMessageType.Unknown;
        }

        private ReadOnlySpan<char> FindHeaderValue(ReadOnlySpan<char> sipMessage, string headerName, StringComparison comparisonType)
        {
            int headerIndex = sipMessage.IndexOf(headerName.AsSpan(), comparisonType);

            if (headerIndex < 0)
                return null;

            int delimiterPosition = -1;
            bool delimiterFound = false;
            int endOfTheLinePosition = -1;
            for (int i = headerIndex; i < sipMessage.Length; i++)
            {
                // find delimiter
                if (!delimiterFound && sipMessage[i] == ':')
                {
                    delimiterPosition = i;
                    delimiterFound = true;
                }

                // find end of the line
                if (sipMessage[i] == '\r' && sipMessage[i + 1] == '\n')
                {
                    endOfTheLinePosition = i - 1;
                    break;
                }
            }

            return sipMessage.Slice(delimiterPosition + 1, endOfTheLinePosition - delimiterPosition);
        }
    }
}

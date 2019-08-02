using System;

namespace Javor.SipSerializer
{
    public class RawSipMessage
    {
        public string SipMessage { get; private set; }

        public RawSipMessage(string sipMessage)
        {
            if (string.IsNullOrEmpty(sipMessage)) throw new ArgumentNullException("Cannt be null or empty");

            SipMessage = sipMessage;
        }

        public string GetHeaderValue(string headerName, StringComparison comparisonType = StringComparison.CurrentCultureIgnoreCase)
        {
            return FindHeaderValue(SipMessage.AsSpan(), headerName, comparisonType).ToString();
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

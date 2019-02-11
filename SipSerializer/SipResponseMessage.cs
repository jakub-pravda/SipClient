using Javor.SipSerializer.Exceptions;
using System;
using System.Text;

namespace Javor.SipSerializer
{
    /// <summary>
    ///     SIP Response message model
    /// </summary>
    public class SipResponseMessage : SipMessage
    {
        /// <summary>
        ///     Initialize new SIP response message.
        /// </summary>
        /// <param name="statusLine">Status line.</param>
        public SipResponseMessage(string statusLine)
        {
            StatusLine = new StatusLine(statusLine);
            Type = SipMessageType.Response;
        }

        /// <summary>
        ///     Initialize new SIP response message.
        /// </summary>
        /// <param name="statusLine">Status line.</param>
        public SipResponseMessage(StatusLine statusLine)
        {
            StatusLine = statusLine;
        }

        public StatusLine StatusLine { get; }

        #region Statics

        public static SipResponseMessage CreateSipResponse(string SipMessage)
        {
            // separate message into the pieces (headers, bodies, ...)
            string[] messageParts
                = SipMessage.Split(new string[] { ABNF.CRLF + ABNF.CRLF }, StringSplitOptions.None);

            // get request line + headers
            string[] statLineAndHeaders
                = messageParts[0].Split(new string[] { ABNF.CRLF }, 2, StringSplitOptions.None);

            SipResponseMessage sipResponse = new SipResponseMessage(statLineAndHeaders[0]);
            sipResponse.AddHeaders(statLineAndHeaders[1]);

            return sipResponse;
        }

        #endregion

        /// <summary>
        ///     Convert SIP response message into the string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(StatusLine.ToString());
            sb.Append(base.ToString());

            return sb.ToString();
        }
    }
}
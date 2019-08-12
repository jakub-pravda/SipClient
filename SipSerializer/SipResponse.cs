using Javor.SipSerializer.Exceptions;
using System;
using System.Text;

namespace Javor.SipSerializer
{
    /// <summary>
    ///     Sip response message
    /// </summary>
    public class SipResponse : BaseSip
    {
        /// <summary>
        ///     Status line
        /// </summary>
        public StatusLine StatusLine { get; private set; }

        /// <summary>
        ///     Initialize new SIP response message
        /// </summary>
        /// <param name="statusLine">Status line</param>
        public SipResponse(string statusLine)
        {
            StatusLine = new StatusLine(statusLine);
            Type = SipMessageType.Response;
        }

        /// <summary>
        ///     Initialize new SIP response message
        /// </summary>
        /// <param name="statusLine">Status line</param>
        public SipResponse(StatusLine statusLine)
        {
            StatusLine = statusLine;
        }


        /// <summary>
        ///     Convert SIP response message into the string
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
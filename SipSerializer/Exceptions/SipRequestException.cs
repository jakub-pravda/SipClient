using System;
using System.Collections.Generic;
using System.Text;

namespace Javor.SipSerializer.Exceptions
{
    /// <summary>
    ///     Sip request message exception.
    /// </summary>
    public class SipRequestException : Exception
    {
        /// <summary>
        ///     Instantiate new sip request message exception.
        /// </summary>
        public SipRequestException()
        {

        }

        /// <summary>
        ///     Instantiate new sip request message exception.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public SipRequestException(string message)
            : base (message)
        {

        }

        /// <summary>
        ///     Instantiate new sip request message exception.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public SipRequestException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}

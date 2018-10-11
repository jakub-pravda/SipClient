using System;

namespace Javor.SipSerializer.Exceptions
{
    /// <summary>
    ///     General sip message exception.
    /// </summary>
    public class SipException : Exception
    {
        /// <summary>
        ///     Instantiate new sip message exception.
        /// </summary>
        public SipException()
        {
            
        }

        /// <summary>
        ///     Instantiate new sip message exception.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public SipException(string message)
            : base(message)
        {
            
        }
        
        /// <summary>
        ///     Instantiate new sip message exception.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public SipException(string message, Exception inner)
            : base(message, inner)
        {
            
        }
    }
}
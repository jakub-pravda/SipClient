using System;

namespace Javor.SipSerializer.Exceptions
{
    /// <summary>
    ///     Sip message parsing exception.
    /// </summary>
    public class SipParsingException : Exception
    {
        /// <summary>
        ///     Initialize new sip emssage parsing exception.
        /// </summary>
        public SipParsingException()
        {
            
        }

        /// <summary>
        ///     Initialize new sip emssage parsing exception.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public SipParsingException(string message)
            : base(message)
        {
            
        }
        
        /// <summary>
        ///     Initialize new sip emssage parsing exception.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public SipParsingException(string message, Exception inner)
            : base(message, inner)
        {
            
        }
        
        
    }
}
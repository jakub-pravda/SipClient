using Javor.SipSerializer.Attributes;

namespace Javor.SipSerializer.HeaderFields
{
    /// <summary>
    ///     Sip header.
    /// </summary>
    public abstract class SipHeader
    {
        /// <summary>
        ///     Initiate new sip header.
        /// </summary>
        protected SipHeader()
        {

        }

        /// <summary>
        ///     Initiate new sip header.
        /// </summary>
        /// <param name="headerContent">Sip header content.</param>
        protected SipHeader(string headerContent)
        {
            OriginalString = headerContent;
        }
        
        /// <summary>
        ///     Original string of sip header content.
        /// </summary>
        public string OriginalString { get; protected set; }
    }
}
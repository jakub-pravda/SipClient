using Javor.SipSerializer.Schemes;

namespace Javor.SipSerializer.HeaderFields
{
    /// <summary>
    ///     "From" header field/**/
    /// </summary>
    public class From : SipHeader
    {
        /// <summary>
        ///     Initialize new From header.
        /// </summary>
        public From()
            : base()
        {
            
        }

        /// <summary>
        ///     Initialize new From header.
        /// </summary>
        /// <param name="fromHeader">From header content.</param>
        public From(string fromHeader)
            : base (fromHeader)
        {
            
        }
        
        public URI URI { get; set; }
        public string DisplayName { get; set; }
        public Scheme Scheme { get; set; }
        public string Tag { get; set; }
    }
}
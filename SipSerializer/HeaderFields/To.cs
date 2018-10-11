using Javor.SipSerializer.Schemes;

namespace Javor.SipSerializer.HeaderFields
{
    /// <summary>
    ///     "To" header specifies the logical recipient of the request
    /// </summary>
    public class To : SipHeader
    {
        /// <summary>
        ///     Initialize new To header.
        /// </summary>
        public To()
            : base()
        {

        }

        /// <summary>
        ///     Initialize new To header.
        /// </summary>
        /// <param name="toHeader">To header content.</param>
        public To(string toHeader)
            : base(toHeader)
        {

        }

        public Scheme Scheme { get; set; }
        public URI URI { get; set; }
        public string DisplayName { get; set; }
    }
}
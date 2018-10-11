using Javor.SdpSerializer;

namespace Javor.SipSerializer.Bodies
{
    /// <summary>
    ///     Session description protocol definition for sip body.
    /// </summary>
    public class SDP : ISipBody
    { 
        /// <summary>
        ///     Initialize new SDP body.
        /// </summary>
        /// <param name="content">SDP message.</param>
        public SDP(SessionDescription content)
        {
            Content = content;
        }
        
        /// <summary>
        ///     Session description.
        /// </summary>
        public SessionDescription Content { get; }

        /// <inheritdoc />
        public string GetMediaType()
        {
            return SdpConstants.MediaType;
        }

        /// <inheritdoc />
        public string SerializeBody()
        {
            return Content.ToString();
        }
    }
}
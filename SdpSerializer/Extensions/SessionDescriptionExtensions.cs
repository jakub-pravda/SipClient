using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Text;
using Javor.SdpSerializer.Specifications;

namespace Javor.SdpSerializer.Extensions
{
    /// <summary>
    ///     Extensions for session description objects.
    /// </summary>
    public static class SessionDescriptionExtensions
    {
        /// <summary>
        ///     Encode session description object.
        /// </summary>
        /// <param name="sessionDescription">Session description object.</param>
        /// <returns>Encoded SDP.</returns>
        public static string Encode(this SessionDescription sessionDescription)
        {
            StringBuilder sb = new StringBuilder();
            
            // v - protocol version
            sb.AppendSdpField(sessionDescription.Version);
            
            // o - originator
            sb.AppendSdpField(sessionDescription.Origin);
            
            // s - session name
            sb.AppendSdpField(sessionDescription.SessionName);
            
            // c - connection data
            sb.AppendSdpField(sessionDescription.ConnectionData);
            
            // b - bandwith
            sb.AppendSdpField(sessionDescription.Bandwidth);
            
            // t - timming
            sb.AppendSdpField(sessionDescription.Timing);

            // a - session aatributes
            sb.AppendSdpField(sessionDescription.SessionAttributes);
            
            // m - media
            sb.AppendSdpField(sessionDescription.Media);

            return sb.ToString();
        }
    }
}
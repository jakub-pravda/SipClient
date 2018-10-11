using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Javor.SdpSerializer.Attributes;
using Javor.SdpSerializer.Exceptions;
using Javor.SdpSerializer.Helpers;

namespace Javor.SdpSerializer.Specifications
{
    /// <summary>
    ///     Media SDP specification.
    /// </summary>
    [DescriptionField('m')]
    public class SessionMedia : SdpField
    {        
        /// <summary>
        ///     Session media type.
        /// </summary>
        public string MediaType { get; set; }
        
        /// <summary>
        ///     Transport port to which the media stream is sent,
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        ///     Number of used ports 
        /// </summary>
        ///  <example>
        ///     When for example port 49170 is defined in the Port property and number of ports is set to 2, then
        ///       a receiver of this message must assume that for communication ports 49170 and 49171 will be used.
        ///       For RTP, the default is that only even numbered ports for data with the corresponding one higher odd
        ///       port used for the RTCP belonging to the RTP session and the number of ports denoting the number of RTP
        ///       sessions.
        /// </example>
        public int NumberOfPorts { get; set; }

        /// <summary>
        ///     Type of the transport protocol.
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        ///     Media format description.
        /// </summary>
        public ICollection<string> Fmt { get; set; }

        /// <summary>
        ///     Media level attributes.
        /// </summary>
        public ICollection<SessionAttribute> MediaAttributes { get; private set; }

        /// <summary>
        ///     Initialize new SDP media specification.
        /// </summary>
        public SessionMedia()
        {
            
        }

        /// <summary>
        ///     Initialize new SDP media specification.
        /// </summary>
        /// <param name="mediaType">Session media type.</param>
        /// <param name="port">Transport port to which the media stream is sent,</param>
        /// <param name="protocol">Type of the transport protocol.</param>
        /// <exception cref="ArgumentNullException">Incalid argument.</exception>
        /// <exception cref="ArgumentException">Invalid argument.</exception>
        public SessionMedia(string mediaType, int port, string protocol)
        {
            if (string.IsNullOrEmpty(mediaType)) 
                throw new ArgumentNullException("Invalid media type.");
            if (!SpecificationHelpers.CheckPortValidity(port))
                throw new ArgumentException("Invalid port.");
            if (string.IsNullOrEmpty(protocol))
                throw new ArgumentNullException("Invalid transport protocol.");
            
            MediaType = mediaType;
            Port = port;
            Protocol = protocol;
        }
    
        /// <summary>
        ///     Initialize new SDP media specification.
        /// </summary>
        /// <param name="mediaType">Session media type.</param>
        /// <param name="port">Transport port to which the media stream is sent,</param>
        /// <param name="protocol">Type of the transport protocol.</param>
        /// <param name="fmt">Media format description values.</param>
        public SessionMedia(string mediaType, int port, string protocol, params string[] fmt)
            : this(mediaType, port, protocol)
        {
            Fmt = fmt;
        }

        public void AddMediaFlagAttribute(string flag)
        {
            AddMediaAttribute(new SessionAttribute(flag));
        }

        public void AddMediaValueAttribute(string attribute, string value)
        {
            AddMediaAttribute(new SessionAttribute(attribute, value));
        }

        public void AddMediaAttribute(SessionAttribute mediaAttribute)
        {
            if (MediaAttributes == null)
            {
                MediaAttributes = new List<SessionAttribute>();
            }
            
            MediaAttributes.Add(mediaAttribute);
        }

        /// <inheritdoc />
        public override string Encode()
        {
            string space = " ";
            StringBuilder sb = new StringBuilder();
            
            sb.Append(SdpType);

            sb.Append(MediaType);
            sb.Append(space);

            sb.Append(Port.ToString());
            sb.Append(space);

            sb.Append(Protocol);
            sb.Append(space);

            foreach (string item in Fmt)
            {
                sb.Append(item);
                sb.Append(space);
            }
            
            // remove last space
            sb.Length--;
            
            // add media attributes
            if (MediaAttributes != null)
            {
                foreach (SessionAttribute mediaAttribute in MediaAttributes)
                {
                    sb.AppendLine(mediaAttribute.Encode());
                }
            }

            return sb.ToString();
        }
    }
}
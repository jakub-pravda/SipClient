using System;
using System.Net;

namespace Javor.SipSerializer.HeaderFields
{
    /// <summary>
    ///     "Via" header field
    /// </summary>
    public class Via : SipHeader
    {
        /// <summary>
        ///     Initialize new Via header.
        /// </summary>
        public Via()
        {

        }

        /// <summary>
        ///     Initialize new Via header.
        /// </summary>
        /// <param name="viaHeader">Via header content.</param>
        public Via(string viaHeader)
            : base(viaHeader)
        {
        }

        public string Version { get; set; }
        public Guid Branch { get; set; }
        public TransportProtocol TransportProtocol { get; set; }
        public IPAddress IPAddress { get; set; }
        public UInt16 Port { get; set; }

        /// <summary>
        ///     Convert Via header into the string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (!String.IsNullOrEmpty(OriginalString))
            {
                return OriginalString;
            }

            throw new NotImplementedException("Via string conversion not yet implemented.");
        }
    }
}
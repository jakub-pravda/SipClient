using System;
using System.Net;

namespace Javor.SipSerializer.Schemes
{
    /// <summary>
    ///     URI model
    /// </summary>
    public class URI
    {
        public URI()
        {
            Port = 5060;
        }

        /// <summary>
        ///     SIP scheme
        /// </summary>
        public Scheme Scheme { get; set; }
        /// <summary>
        ///     User name (eg. extension, name, etc.)
        /// </summary>
        public string User { get; set; }
        /// <summary>
        ///     Domain name
        /// </summary>
         public string DomainName { get; set; }
        /// <summary>
        ///     URI IPAddress host
        /// </summary>
        public IPAddress Host { get; set; }
        /// <summary>
        ///     URI IPAddress port
        /// </summary>
        public UInt16 Port { get; set; }

        /// <summary>
        ///     URI string representation
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string domain;
            if (DomainName != null)     // use domain name as domain
            {
                domain = DomainName;
            }
            else                        // use ip addr with port as domain
            {
                domain = Host.ToString() + ":" + Port.ToString();
            }

            return Scheme.ToString() + ":" + User + "@" + domain;
        }
    }
}
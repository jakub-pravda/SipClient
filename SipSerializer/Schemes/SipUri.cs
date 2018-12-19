using System;
using System.Net;
using System.Text;

namespace Javor.SipSerializer.Schemes
{
    /// <summary>
    ///     URI model
    /// </summary>
    public class SipUri
    {
        public SipUri()
        {
            Port = 5060;
        }

        /// <summary>
        ///     Initialize new sip uri.
        /// </summary>
        /// <param name="host"></param>
        public SipUri(string host)
            : this()
        {
            if (host == null) throw new ArgumentNullException("Invalid sip host.");

            Host = host;
        }

        /// <summary>
        ///     Initialize new sip uri.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public SipUri(string host, int port)
            : this(host)
        {
            Port = port;
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
        ///     Sip host. Should be both, domain or ip address.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        ///     URI IPAddress port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        ///     URI string representation
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            // sip uri format - sip:user:password@host:port
            StringBuilder sb = new StringBuilder();
            sb.Append(Scheme.ToString());
            sb.Append(":");
            sb.Append(User);
            sb.Append("@");
            sb.Append(Host);

            if (Port != 5060) // non standard sip port
            {
                sb.Append(":");
                sb.Append(Port.ToString());
            }

            return sb.ToString();
        }
    }
}
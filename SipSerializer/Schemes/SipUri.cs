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
        public SipUri(string host, string user)
            : this()
        {
            if (host == null) throw new ArgumentNullException("Invalid sip uri host.");
            if (string.IsNullOrEmpty(user)) throw new ArgumentNullException("Invalid sip uri user.");

            Host = host;
            User = user;
        }

        /// <summary>
        ///     Initialize new sip uri.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public SipUri(string host, int port, string user)
            : this(host, user)
        {
            Port = port;
        }

        public SipUri(string sipUri)
        {
            PrivateDeserializeSipuri(sipUri);
        }

        public const string SipScheme = "sip";
        public const string SipsScheme = "sips";

        /// <summary>
        ///     SIP scheme
        /// </summary>
        public string Scheme { get; set; }
            = SipScheme;

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

        private void PrivateDeserializeSipuri(string sipUri)
        {
            // sip:80362@algocloud.net
            string[] splitted = sipUri.Split(new char[] {':', '@'});

            Scheme = splitted[0];
            User = splitted[1];
            Host = splitted[2];

            if (splitted.Length > 3)
                Port = int.Parse(splitted[3]);
        }
    }
}
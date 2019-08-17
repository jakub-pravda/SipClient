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
        /// <summary>
        ///     Initialize new sip uri
        /// </summary>
        /// <param name="host"></param>
        public SipUri(string host, string user, string scheme = SipsScheme, int port = 5060)
        {
            if (string.IsNullOrEmpty(host)) throw new ArgumentNullException("Invalid sip uri host.");
            if (string.IsNullOrEmpty(user)) throw new ArgumentNullException("Invalid sip uri user.");

            Host = host;
            User = user;
            Port = port;
            Scheme = scheme;
        }

        public const string SipScheme = "sip";
        public const string SipsScheme = "sips";

        /// <summary>
        ///     SIP scheme
        /// </summary>
        public string Scheme { get; private set; }
            = SipScheme;

        /// <summary>
        ///     User name (eg. extension, name, etc.)
        /// </summary>
        public string User { get; private set; }

        /// <summary>
        ///     Sip host. Should be both, domain or ip address.
        /// </summary>
        public string Host { get; private set; }

        /// <summary>
        ///     URI IPAddress port
        /// </summary>
        public int Port { get; private set; }

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

        public static SipUri Parse(string s)
        {
            // sip:80362@algocloud.net
            string[] splitted = s.Split(new char[] { ':', '@' });

            string scheme = splitted[0];
            string user = splitted[1];
            string host = splitted[2];

            if (splitted.Length > 3)
                // with port
                return new SipUri(host, user, scheme, int.Parse(splitted[3]));

            return new SipUri(host, user, scheme);
        }
    }
}
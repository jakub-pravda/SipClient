using Javor.SipSerializer.Exceptions;
using Javor.SipSerializer.Schemes;
using System;

namespace Javor.SipSerializer
{
    /// <summary>
    ///     Start line for requests
    /// </summary>
    public struct RequestLine
    {

        /// <summary>
        ///     Request method.
        /// </summary>
        public string Method;

        /// <summary>
        ///     Remote host identification.
        /// </summary>
        public string Host;

        /// <summary>
        ///     Sip scheme identification.
        /// </summary>
        public string Scheme;

        /// <summary>
        ///     Sip version.
        /// </summary>
        public string Version;

        /// <summary>
        ///     Fill request line structure.
        /// </summary>
        /// <param name="method">Request method.</param>
        /// <param name="uri">Request Uri.</param>
        /// <param name="version">Sip version.</param>
        public RequestLine(string method, SipUri uri, string version)
        {
            Method = method;
            Version = version;
            Host = uri.Host;
            Scheme = uri.Scheme;
        }

        /// <summary>
        ///     Convert Sip request into the ASCII format.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Method} {Scheme}:{Host} {Version}{ABNF.CRLF}";
        }
    }

}
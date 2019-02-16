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
        ///     Request uri.
        /// </summary>
        public SipUri Uri;

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
            Uri = uri;
            Version = version;
        }

        /// <summary>
        ///     Convert Sip request into the ASCII format.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Method} {Uri} {Version} {ABNF.CRLF}";
        }
    }

}
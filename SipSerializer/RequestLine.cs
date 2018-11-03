using Javor.SipSerializer.Exceptions;
using Javor.SipSerializer.Helpers;
using System;

namespace Javor.SipSerializer
{
    /// <summary>
    ///     Start line for requests
    /// </summary>
    public class RequestLine
    {
        /// <summary>
        ///     Initialize new request line.
        /// </summary>
        public RequestLine(string requestMethod, string destinationUri)
        {
            if (requestMethod == null || destinationUri == null)
                throw new ArgumentNullException("Input parameters cann't be null.");

            Method = requestMethod;
            Uri = new Uri(destinationUri);
        }

        public RequestLine(string requestMethod, Uri destinationUri)
        {
            if (requestMethod == null || destinationUri == null)
                throw new ArgumentNullException("Input parameters cann't be null.");

            Method = requestMethod;
            Uri = destinationUri;
        }

        /// <summary>
        ///     Initialize new request line.
        /// </summary>
        /// <param name="requestLine">Request line in raw format.</param>
        public RequestLine(string requestLine)
        {
            DeserializeRequestLine(requestLine);
            OriginalString = requestLine;
        }
        
        /// <summary>
        ///     Request method.
        /// </summary>
        public string Method { get; set; }
        
        /// <summary>
        ///     Request uri.
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        ///     Sip version.
        /// </summary>
        public string Version { get; } = Constants.SipVersion;

        /// <summary>
        ///     Raw format of the request line.
        /// </summary>
        public string OriginalString { get; private set; }

        private void DeserializeRequestLine(string requestLine)
        {
            var parsed = requestLine.Trim().Split(ABNF.SP);

            if (!SipParsingHelpers.IsRequestLine(parsed, out string err))
            {
                throw new SipParsingException(err);
            }
            
            Method = parsed[0];
            Uri = new Uri(parsed[1]);
        }

        /// <summary>
        ///     Convert Sip request into the ASCII format.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Method} {Uri.AbsoluteUri} {Version}";
        }
    }

}
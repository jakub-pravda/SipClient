using Javor.SipSerializer.Exceptions;
using Javor.SipSerializer.Helpers;
using System;

namespace Javor.SipSerializer
{
    /// <summary>
    ///     Response status line.
    /// </summary>
    public class StatusLine
    {
        /// <summary>
        ///     Initialize new status line.
        /// </summary>
        /// <param name="statusCode"></param>
        public StatusLine(StatusCode statusCode)
        {
            Version = Constants.SipVersion;

            Tuple<string, string> translated = Translators.StatusCodeTranslator[statusCode];
            StatusCode = translated.Item1;
            ReasonPhrase = translated.Item2;
        }

        /// <summary>
        ///     Initialize new response status line.
        /// </summary>
        /// <param name="statusLine">String status line.</param>
        public StatusLine(string statusLine)
        {
            OriginalString = statusLine;
            DeserializeStatusLine(statusLine);
        }

        /// <summary>
        ///     Version
        /// </summary>
        public string Version { get; }

        /// <summary>
        ///     Status-Code
        /// </summary>
        public string StatusCode { get; private set; }

        /// <summary>
        ///     Reason-Phrase
        /// </summary>
        public string ReasonPhrase { get; private set; }

        /// <summary>
        ///     Raw format of the status line.
        /// </summary>
        public string OriginalString { get; set; }

        private void DeserializeStatusLine(string statusLine)
        {
            var parsed = statusLine.Trim().Split(ABNF.SP);

            if (!ParsingHelpers.IsStatusLine(parsed, out string err))
            {
                throw new SipParsingException(err);
            }

            StatusCode = parsed[1];
            ReasonPhrase = parsed[2];
        }

        /// <summary>
        ///     Convert status line into the string.
        /// </summary>
        /// <returns>String representation of status line.</returns>
        public override string ToString()
        {
            return $"{Version} {StatusCode} {ReasonPhrase}"; 
        }
    }
}
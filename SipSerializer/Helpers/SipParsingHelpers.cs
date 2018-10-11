using System;

namespace Javor.SipSerializer.Helpers
{
    public static class SipParsingHelpers
    {
        public static bool ValidateSipVersion(string sipVersion)
        {
            return sipVersion == Constants.SipVersion;
        }

        /// <summary>
        ///     Get sip message type.
        /// </summary>
        /// <param name="sipMessage">Raw sip message.</param>
        /// <returns>Sip message type.</returns>
        public static SipMessage.SipMessageType GetSipMessageType(string sipMessage)
        {
            // TODO error handling

            // Get first line of the incomming message.
            //  First line should be status/request line based on type of the
            //  sip message.
            string initLine = sipMessage.Split(new string[] { ABNF.CRLF }, 2, StringSplitOptions.None)[0];

            // check if init line is status or request line
            if (IsRequestLine(initLine, out string reqErr))
            {
                return SipMessage.SipMessageType.Request;
            }
            else if (IsStatusLine(initLine, out string stErr))
            {
                return SipMessage.SipMessageType.Response;
            }

            return SipMessage.SipMessageType.Unknown;
        }

        /// <summary>
        ///     Check whether string is valid status line.
        /// </summary>
        /// <param name="statusLine">Sip response status line.</param>
        /// <param name="errorMessage">Error message.</param>
        /// <returns>True if provided string is status line. False otherwise.</returns>
        public static bool IsStatusLine(string statusLine, out string errorMessage)
        {
            string[] parsed = statusLine.Trim().Split(ABNF.SP);

            bool result = IsStatusLine(parsed, out string err);

            errorMessage = err;
            return result;
        }

        /// <summary>
        ///     Check whether string is valid status line.
        /// </summary>
        /// <param name="statusLine">Sip response status line.</param>
        /// <param name="errorMessage">Error message.</param>
        /// <returns>True if provided string is status line. False otherwise.</returns>
        public static bool IsStatusLine(string[] statusLine, out string errorMessage)
        {
            if (statusLine.Length != 3)
            {
                errorMessage = "Invalid format of the status line.";
                return false;
            }

            // at Sip response Sip version field is at first position
            if (!ValidateSipVersion(statusLine[0]))
            {
                errorMessage = $"Invalid sip version in the status line {statusLine[0]}. Should be {Constants.SipVersion}.";
                return false;
            }

            errorMessage = null;
            return true;
        }

        /// <summary>
        ///     Check whether string is valid request line.
        /// </summary>
        /// <param name="requestLine">Sip request line.</param>
        /// <param name="errorMessage">Error message.</param>
        /// <returns>True if provided string is request line. False otherwise.</returns>
        public static bool IsRequestLine(string requestLine, out string errorMessage)
        {
            string[] parsed = requestLine.Trim().Split(ABNF.SP);

            bool result = IsRequestLine(parsed, out string err);

            errorMessage = err;
            return result;
        }

        /// <summary>
        ///     Check whether string is valid request line.
        /// </summary>
        /// <param name="requestLine">Parsed sip request line.</param>
        /// <param name="errorMessage">Error message.</param>
        /// <returns>True if provided string is request line. False otherwise.</returns>
        public static bool IsRequestLine(string[] requestLine, out string errorMessage)
        {
            if (requestLine.Length != 3)
            {
                errorMessage = "Invalid format of the request line.";
                return false;
            }
            else if (!ValidateSipVersion(requestLine[2]))
            {
                errorMessage = $"Invalid sip version in the request line {requestLine[2]}. Should be {Constants.SipVersion}.";
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}
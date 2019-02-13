namespace Javor.SipSerializer
{
    public static class Constants
    {
        /// <summary>
        ///     RFC 3261 Sip version.
        /// </summary>
        public const string SipVersion = "SIP/2.0";
    }

    public static class RequestMethods
    {
        /// <summary>
        ///     Invite method.
        /// </summary>
        public const string Invite = "INVITE";

        /// <summary>
        ///     Cancel.
        /// </summary>
        public const string Cancel = "CANCEL";


        /// <summary>
        ///     Register.
        /// </summary>
        public const string Register = "REGISTER";

        /// <summary>
        ///     Options.
        /// </summary>
        public const string Options = "OPTIONS";
    }

    public static class ResponseCodes 
    {
        #region 2xx successful responses
        /// <summary>
        ///     200 OK status code.
        /// </summary>
        public const string StatusCode200Ok = "200";

        /// <summary>
        ///     202 ACCEPTED status code.
        /// </summary>
        public const string StatusCode202Accepted = "202";
        #endregion
    }
}
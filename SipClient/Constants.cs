namespace SipClient
{
    public static class Defaults
    {
        public static string[] AllowedMethods = new string[]
        {
            "INVITE",
            "ACK",
            "OPTIONS",
            "BYE"
        };

        /// <summary>
        ///     Minimum buffer size for listening.
        /// </summary>
        public const int MinimumBufferSize = 512;
    }
}
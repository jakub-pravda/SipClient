namespace Javor.SdpSerializer
{
    public static class SdpConstants
    {
        /// <summary>
        ///     SDP media type.
        /// </summary>
        public const string MediaType = "application/sdp";

        /// <summary>
        ///     SDP new line definition.
        /// </summary>
        public const string SdpNewLine = "\r\n";

        /// <summary>
        ///     SDP type value delimiter.
        /// </summary>
        public const string TypeDelimiter = "=";
    }

    /// <summary>
    ///     Definition of media types.
    /// </summary>
    public static class MediaTypes
    {
        /// <summary>
        ///     Audio media type.
        /// </summary>
        public const string AudioMediaType = "audio";
        
        /// <summary>
        ///     Video media type.
        /// </summary>
        public const string VideoMediaType = "video";
        
        /// <summary>
        ///     Text media type.
        /// </summary>
        public const string TextMediaType = "text";
        
        /// <summary>
        ///     Application media type.
        /// </summary>
        public const string ApplicationType = "application";
    }

    /// <summary>
    ///     Bandwith types.
    /// </summary>
    public static class BandwithTypes
    {
        /// <summary>
        ///     Conference total (CT) bandwith type.
        /// </summary>
        public const string ConferenceTotal = "CT";

        /// <summary>
        ///     Application specific (AS) bandwith type.
        /// </summary>
        public const string ApplicationSpecific = "AS";
    }


    /// <summary>
    ///     Media transport protocols.
    /// </summary>
    public static class MediaTransportProtocols
    {
        /// <summary>
        ///     Denotes unspecified protocol running under UDP.
        /// </summary>
        public const string Udp = "udp";

        /// <summary>
        ///     Denotes RTP for audio and video.
        /// </summary>
        public const string RtpAvp = "RTP/AVP";

        /// <summary>
        ///     Denotes secure RTP for aduio and video.
        /// </summary>
        public const string RtpSavp = "RTP/SAVP";
    }
}
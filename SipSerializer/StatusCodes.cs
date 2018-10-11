using System;
using System.Collections.Generic;
using System.Text;

namespace Javor.SipSerializer
{
    /// <summary>
    ///     Sip message validity codes.
    /// </summary>
    public enum SipMessageValidityCode
    {
        /// <summary>
        ///     Valid sip message.
        /// </summary>
        Valid
    }

    /// <summary>
    ///     Sip status codes.
    /// </summary>
    public enum StatusCode
    {
        Ok = 200,
        BadRequest = 400,
        BadExtension = 420
    }

    public static class Translators
    {
        public static Dictionary<StatusCode, Tuple<string, string>> StatusCodeTranslator =
            new Dictionary<StatusCode, Tuple<string, string>>()
            {
                { StatusCode.Ok, new Tuple<string, string>("200", "OK")},
                { StatusCode.BadRequest, new Tuple<string, string>("400", "Bad Request")},
                { StatusCode.BadExtension, new Tuple<string, string>("420", "Bad Extension")}
            };
    }
}

using System.IO.Compression;

namespace Javor.SipSerializer.Bodies
{
    /// <summary>
    ///     Sip body definition.
    /// </summary>
    public interface ISipBody
    {
        /// <summary>
        ///     Get body media type.
        /// </summary>
        /// <returns>Body media type.</returns>
        string GetMediaType();

        /// <summary>
        ///     Serialize body into the form suitable for message transfering. 
        /// </summary>
        /// <returns></returns>
        string SerializeBody();
    }
}
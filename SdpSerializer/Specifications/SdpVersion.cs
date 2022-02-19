using System;
using System.Text;
using Javor.SdpSerializer.Attributes;
using Javor.SdpSerializer.Exceptions;
using Javor.SdpSerializer.Helpers;

namespace Javor.SdpSerializer.Specifications
{
    /// <summary>
    ///     SDP version.
    /// </summary>
    [SdpMandatory]
    [DescriptionField('v')]
    public class SdpVersion : SdpField
    {
        /// <summary>
        ///     Version value,
        /// </summary>
        public string Value { get; set; } = "0";
        
        /// <summary>
        ///     Initialize new SDP version with default value (0).
        /// </summary>
        public SdpVersion()
        {
        }
        
        /// <summary>
        ///     Initialize new SDP version with.
        /// </summary>
        /// <param name="version">SDP version.</param>
        public SdpVersion(string version)
        {
            Value = version;
        }
        
        /// <inheritdoc />
        public override string Encode()
        {
            return $"{SdpType}{Value}";
        }
    }
}
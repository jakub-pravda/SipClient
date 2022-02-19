using System;
using System.Text;
using Javor.SdpSerializer.Attributes;
using Javor.SdpSerializer.Exceptions;
using Javor.SdpSerializer.Helpers;

namespace Javor.SdpSerializer.Specifications
{
    /// <summary>
    ///     Session name SDP specification.
    /// </summary>
    [SdpMandatory]
    [DescriptionField('s')]
    public class SessionName : SdpField
    {
        /// <summary>
        ///     Get session name value.
        /// </summary>
        public string Value { get; set; } = "-";

        /// <summary>
        ///     Initialize new session name with default value.
        /// </summary>
        public SessionName()
        {
        }

        /// <summary>
        ///     Initialize new session name.
        /// </summary>
        /// <param name="sessionName">Session name.</param>
        public SessionName(string sessionName)
        {
            Value = sessionName;
        }

        /// <inheritdoc />
        public override string Encode()
        {
            return $"{SdpType}{Value}";
        }
    }
}
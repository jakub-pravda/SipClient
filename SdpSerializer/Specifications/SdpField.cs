using System;
using System.Reflection;
using System.Text;
using Javor.SdpSerializer.Attributes;
using Javor.SdpSerializer.Helpers;

namespace Javor.SdpSerializer.Specifications
{
    /// <summary>
    ///     Base SDP field definiton. All SDP fields must inherit from this abstract class.
    /// </summary>
    public abstract class SdpField
    {
        /// <summary>
        ///     Initilize new SDP field definition.
        /// </summary>
        public SdpField()
        {
            SdpType = SpecificationHelpers.GetDescriptionField(this);
        }

        public string SdpType { get; }

        /// <summary>
        ///     Original SDP field.
        /// </summary>
        public string OriginalField { get; set; }

        /// <summary>
        ///     Encode Sdp field into SDP specification relevant record.
        /// </summary>
        /// <returns>Encoded sdp field.</returns>
        public abstract string Encode();
    }
}
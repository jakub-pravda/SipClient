using System;
using System.Text;
using Javor.SdpSerializer.Attributes;
using Javor.SdpSerializer.Exceptions;
using Javor.SdpSerializer.Helpers;

namespace Javor.SdpSerializer.Specifications
{
    /// <summary>
    ///     Bandwith SDP specification.
    /// </summary>
    [DescriptionField('b')]
    public class SessionBandwidth : SdpField
    {
        /// <summary>
        ///     Initialize new bandwidth definition.
        /// </summary>
        public SessionBandwidth()
        {
            
        }
        
        /// <summary>
        ///     Initialize new bandwith definition.
        /// </summary>
        /// <param name="bandwidthType">
        ///     Bandwith type. There are two bandwith types defined by SDP specificaion: CT and AS.
        /// </param>
        /// <param name="bandwith">Bandwith value in kilobits per second.</param>
        public SessionBandwidth(string bandwidthType, int bandwith)
        {
            BandwidthType = bandwidthType;
            Value = bandwith;
        }
        
        /// <summary>
        ///     Type of used bandwith. There are two standard bandwith types: CT and AS.
        /// </summary>
        public string BandwidthType { get; set; }

        /// <summary>
        ///     Bandwith value in kilobits per second.
        /// </summary>
        public int Value { get; set; }

        /// <inheritdoc />
        public override string Encode()
        {
            string descriptionField = SdpType;
            return $"{descriptionField}{BandwidthType}:{Value.ToString()}";
        }
    }
}
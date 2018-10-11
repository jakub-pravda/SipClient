using System;
using System.Text;
using Javor.SdpSerializer.Attributes;
using Javor.SdpSerializer.Exceptions;
using Javor.SdpSerializer.Helpers;

namespace Javor.SdpSerializer.Specifications
{
    /// <summary>
    ///     Timing SDP specification
    /// </summary>
    [SdpMandatory]
    [DescriptionField('t')]
    public class SessionTiming : SdpField
    {
        /// <summary>
        ///     Initialize new sdp timing field.
        /// </summary>
        public SessionTiming()
        {
        }

        /// <summary>
        ///     Initialize new sdp timing field.
        /// </summary>
        /// <param name="startTime">Session start time in NTP format.</param>
        /// <param name="stopTime">Session stop time int NTP format.</param>
        public SessionTiming(int startTime, int stopTime)
        {
            StartTime = startTime;
            StopTime = stopTime;
        }

        /// <summary>
        ///     Session start time in NTP format. Defaultly set to 0.
        /// </summary>
        public int StartTime { get; set; } = 0;
        
        /// <summary>
        ///     Session stop time in NTP format. Defaultly set to 0.
        /// </summary>
        public int StopTime { get; set; } = 0;
        
        /// <inheritdoc />
        public override string Encode()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append(SdpType);
            sb.Append(StartTime.ToString());
            sb.Append(":");
            sb.Append(StopTime.ToString());

            return sb.ToString();
        }
    }
}
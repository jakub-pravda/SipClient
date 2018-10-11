using System;
using System.Collections.Generic;
using System.Text;
using Javor.SdpSerializer.Exceptions;
using Javor.SdpSerializer.Helpers;
using Javor.SdpSerializer.Specifications;

namespace Javor.SdpSerializer.Extensions
{
    /// <summary>
    ///     Extensions for string builder.
    /// </summary>
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendSdpField(this StringBuilder sb, SdpField sdpField)
        {
            if (sdpField == null)
            {
                // TODO check field manatory
                
                return sb;
            }

            sb.Append(sdpField.Encode());
            sb.Append(SdpConstants.SdpNewLine);

            return sb;
        }
        
        public static StringBuilder AppendSdpField(this StringBuilder sb, IEnumerable<SdpField> sdpFields)
        {
            foreach (SdpField sdpField in sdpFields)
            {
                sb.AppendSdpField(sdpField);
            }

            return sb;
        }
    }
}
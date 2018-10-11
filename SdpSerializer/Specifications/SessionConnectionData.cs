using System;
using System.Text;
using Javor.SdpSerializer.Attributes;
using Javor.SdpSerializer.Exceptions;
using Javor.SdpSerializer.Helpers;

namespace Javor.SdpSerializer.Specifications
{
    /// <summary>
    ///     SDP connection data.
    /// </summary>
    [DescriptionField('c')]
    public class SessionConnectionData : SdpField
    {
        /// <summary>
        ///     Initialize new SDP connection data field.
        /// </summary>
        public SessionConnectionData()
        {
            
        }

        /// <summary>
        ///     Initialize new SDP connection data field.
        /// </summary>
        /// <param name="netType">Net type.</param>
        /// <param name="addressType">Address type.</param>
        /// <param name="connectionAddress">Connection address.</param>
        public SessionConnectionData(string netType, string addressType, string connectionAddress)
        {
            NetType = netType;
            AddressType = addressType;
            ConnectionAddress = connectionAddress;
        }
        
        /// <summary>
        ///     Netwrok type.
        /// </summary>
        public string NetType { get; set; }
        
        /// <summary>
        ///     Address type. IP4 or IP6 are suitable keywords for this.
        /// </summary>
        public string AddressType { get; set; }
        
        /// <summary>
        ///     Connection address. Mostly IP4 or IP6 address.
        /// </summary>
        public string ConnectionAddress { get; set; }

        /// <inheritdoc />
        public override string Encode() 
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(SdpType);
            
            // append nettype
            sb.Append(NetType);
            sb.Append(" ");

            // append addrtype
            sb.Append(AddressType);
            sb.Append(" ");

            // append connection address
            sb.Append(ConnectionAddress);

            return sb.ToString();
        }
    }
}
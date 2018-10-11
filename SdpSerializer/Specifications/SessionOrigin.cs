using System;
using System.Text;
using Javor.SdpSerializer.Attributes;
using Javor.SdpSerializer.Exceptions;
using Javor.SdpSerializer.Helpers;

namespace Javor.SdpSerializer.Specifications
{
    /// <summary>
    ///     Originator of the session.
    /// </summary>
    [SdpMandatory]
    [DescriptionField('o')]
    public class SessionOrigin : SdpField
    {
        /// <summary>
        ///     Initialize new origin.
        /// </summary>
        public SessionOrigin()
        {
        }

        public SessionOrigin(string username, string sessionId, string sessionVersion, string netType, string addressType,
            string unicastAddress)
        {
            Username = username;
            SessionId = sessionId;
            SessionVersion = sessionVersion;
            NetType = netType;
            AddressType = addressType;
            UnicastAddress = unicastAddress;
        }
        
        /// <summary>
        ///     User login on the originating host.
        /// </summary>
        public string Username { get; set; } = "-";

        /// <summary>
        ///     Unique session id.
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        ///     Session version.
        /// </summary>
        public string SessionVersion { get; set; }

        /// <summary>
        ///  TYpe of network.
        /// </summary>
        public string NetType { get; set; }

        /// <summary>
        ///     Address type. IP4 or IP6 are meaningful for this property.
        /// </summary>
        public string AddressType { get; set; }

        /// <summary>
        ///      Address of the machine from which the session was created.
        /// </summary>
        public string UnicastAddress { get; set; }

        /// <inheritdoc />
        public override string Encode()
        {
            string space = " ";
            StringBuilder sb = new StringBuilder();
            
            sb.Append(SdpType);
            
            sb.Append(Username);
            sb.Append(space);
            
            sb.Append(SessionId);
            sb.Append(space); 
            
            sb.Append(SessionVersion);
            sb.Append(space);
            
            sb.Append(NetType);
            sb.Append(space);
            
            sb.Append(AddressType);
            sb.Append(space);
            
            sb.Append(UnicastAddress);

            return sb.ToString();
        }
    }
}
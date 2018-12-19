using System;
using System.Net;
using System.Text;

namespace Javor.SipSerializer.HeaderFields
{
    /// <summary>
    ///     "Via" header field
    /// </summary>
    public class Via : SipHeader
    {
        /// <summary>
        ///     Initialize new Via header.
        /// </summary>
        public Via()
        {

        }

        /// <summary>
        ///     Initialize new Via header.
        /// </summary>
        /// <param name="viaHeader">Full via header in the ascii form.</param>
        public Via(string viaHeader)
            : base(viaHeader)
        {
        }

        /// <summary>
        ///     Initialize new Via header.
        /// </summary>
        /// <param name="ipAddress">Host ip address.</param>
        /// <param name="port">Host port.</param>
        /// <param name="transportProtocol">Communication protocol used by host.</param>
        /// <param name="branch">Branch parameter.</param>
        public Via(string ipAddress, int port, TransportProtocol transportProtocol, string branch = null)
        {
            // TODO branch check + guardian clausses

            IpAddress = ipAddress;
            Port = port;
            TransportProtocol = transportProtocol;

            if (string.IsNullOrEmpty(branch))
            {
                SetNewBranchParameter();
            }
            else
            {
                // branch check
                Branch = branch;
            }
        }
        
        /// <summary>
        ///     Sip version.
        /// </summary>
        public string Version { get; set; } = Constants.SipVersion;

        /// <summary>
        ///     Branch parameter.
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        ///     Transport protocol using by sip sip server/client which issued via header.
        /// </summary>
        public TransportProtocol TransportProtocol { get; set; }

        /// <summary>
        ///     Ip address or hostname of SIP server/client.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        ///     Port which will be used by endpoint to communicate with localhost.
        /// </summary>
        public int Port { get; set; }
       
        /// <summary>
        ///     Convert Via header to the ascii form.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(OriginalString))
            {
                return OriginalString;
            }

            return $"{Version}/{TransportProtocol.ToString()} {IpAddress}:{Port.ToString()};branch={Branch}";
        }


        private void SetNewBranchParameter()
        {
            Branch = string.Format("z9hG4bK{0}", Guid.NewGuid().ToString("N"));
        }

    }
}
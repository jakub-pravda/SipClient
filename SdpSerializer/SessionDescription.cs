using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using Javor.SdpSerializer.Attributes;
using Javor.SdpSerializer.Extensions;
using Javor.SdpSerializer.Specifications;

namespace Javor.SdpSerializer
{    
    /// <summary>
    ///     Session description object.
    /// </summary>
    public class SessionDescription
    {
        /// <summary>
        ///     Initialize new session description.
        /// </summary>
        public SessionDescription()
        {
            
        }

        /// <summary>
        ///     Initialize new session description.
        /// </summary>
        /// <param name="sessionName">Session name.</param>
        public SessionDescription(string sessionName)
        {
            SessionName = new SessionName(sessionName);
        }
        
        /// <summary>
        ///     Version of the session description protocol.
        /// </summary>
        public SdpVersion Version { get; set; } 
            = new SdpVersion();

        /// <summary>
        ///     Originator of the session.
        /// </summary>.
        public SessionOrigin Origin { get; set; }

        /// <summary>
        ///     Session name.
        /// </summary>
        public SessionName SessionName { get; set; } 
            = new SessionName("-");

        /// <summary>
        ///     Connection data.
        /// </summary>
        public SessionConnectionData ConnectionData { get; set; }

        /// <summary>
        ///     Bandwith definition.
        /// </summary>
        public ICollection<SessionBandwidth> Bandwidth { get; private set; }

        /// <summary>
        ///     SDP timing.
        /// </summary>
        public SessionTiming Timing { get; set; }

        /// <summary>
        ///     Session level attributes.
        /// </summary>
        public ICollection<SessionAttribute> SessionAttributes { get; set; }
            = new List<SessionAttribute>();

        /// <summary>
        ///     Session media.
        /// </summary>
        public ICollection<SessionMedia> Media { get; set; }
            = new List<SessionMedia>();

        public void AddBandwidth(SessionBandwidth sessionBandwidth)
        {
            if (Bandwidth == null)
            {
                Bandwidth = new List<SessionBandwidth>();
            }
            
            Bandwidth.Add(sessionBandwidth);
        }
    }
}
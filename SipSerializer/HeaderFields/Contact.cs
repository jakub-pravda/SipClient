using System.ComponentModel;
using Javor.SipSerializer.Schemes;
using Javor.SipSerializer.Attributes;
using System;

namespace Javor.SipSerializer.HeaderFields
{
    /// <summary>
    ///     A "Contact" header field value provides URI whose meaning depends on the type of request 
    ///     or response it is in
    /// </summary> 
    public class Contact
    {        
        public SipUri URI { get; set; }
        public string DisplayName { get; set; }

        [ParameterName("expires")]
        public int Expires { get; set; }    // only valid fo REGISTER requests, responses and for 3xx responses

        [ParameterName("q")]
        public float Q { get; set ;}        // only valid fo REGISTER requests, responses and for 3xx responses

        /// <summary>
        ///     Contacts string representation
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            throw new NotImplementedException();
            // TODO reflection method, napr "expires=3600"
        }
    }
}
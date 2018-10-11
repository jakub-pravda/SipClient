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
    public class Contact : SipHeader
    {
        /// <summary>
        ///     Initialize new contact header.
        /// </summary>
        public Contact()
            : base()
        {
                
        }

        /// <summary>
        ///     Initialize new contact header.
        /// </summary>
        /// <param name="contactHeader">Contact header content.</param>
        public Contact(string contactHeader)
            : base(contactHeader)
        {
            
        }
        
        public URI URI { get; set; }
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
using Javor.SipSerializer.Attributes;
using System;

namespace Javor.SipSerializer.Models
{
    /// <summary>
    ///     Creation options for SIP REGISTER method.
    /// </summary>
    public class SipRegisterOptions
    {
        // sip register mandatory fields
        [Required]
        public Uri RequestUri { get; set; }

        [Required]
        public int SequenceNumber { get; set; }

        [Required]
        public string To { get; set; }

        private string _from;
        public string From
        {
            get
            {
                if (string.IsNullOrEmpty(_from))
                {
                    return To;
                }

                return _from;
            }
            set
            {
                _from = value;
            }
        }

        [Required]
        public string CallId { get; set; }
        public string Contact { get; set; }
    }
}

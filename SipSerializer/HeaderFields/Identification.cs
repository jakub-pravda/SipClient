using Javor.SipSerializer.Schemes;
using System;
using System.Text;

namespace Javor.SipSerializer.HeaderFields
{
    /// <summary>
    ///     Identification (from / to) header field.
    /// </summary>
    public class Identification : SipHeader
    {
        /// <summary>
        ///     Initialize new identification (from / to) header.
        /// </summary>
        public Identification(SipUri sipUri, string displayName = null, string tag = null)
            : base()
        {
            Uri = sipUri ?? throw new ArgumentNullException("Invalid client URI.");
            DisplayName = displayName;
            Tag = tag;
        }

        /// <summary>
        ///     Initialize new identification (from / to) header.
        /// </summary>
        /// <param name="identificationHeader">From header content.</param>
        public Identification(string identificationHeader)
            : base (identificationHeader)
        {
            
        }
        
        public SipUri Uri { get; set; }
        public string DisplayName { get; set; }
        public string Tag { get; set; }

        /// <summary>
        ///     Converts identification header into the ascii format.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(DisplayName))
            {
                sb.Append(string.Format("{0} ", DisplayName));
            }

            sb.Append(string.Format("<{0}>", Uri.ToString()));

            if (!string.IsNullOrEmpty(Tag))
            {
                sb.Append(string.Format(";tag={0}", Tag));
            }

            return sb.ToString(); 
        }
    }
}
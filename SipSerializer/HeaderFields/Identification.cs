using Javor.SipSerializer.Attributes;
using Javor.SipSerializer.Schemes;
using System;
using System.Text;

namespace Javor.SipSerializer.HeaderFields
{
    /// <summary>
    ///     Identification (from / to) header field.
    /// </summary>
    public class Identification
    {
        /// <summary>
        ///     Initialize new identification (from / to) header.
        /// </summary>
        /// <param name="sipUri">From/To identification.</param>
        /// <param name="tag">Tag id.</param>
        /// <param name="displayName">Display name parameter.</param>
        public Identification(SipUri sipUri, string tag, string displayName = null)
        {
            Uri = sipUri ?? throw new ArgumentNullException("Invalid client URI.");
            DisplayName = displayName;
            Tag = tag;
        }
        
        /// <summary>
        ///     From / To identification.
        /// </summary>
        [Required]
        public SipUri Uri { get; set; }

        /// <summary>
        ///     Display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Tag id.
        /// </summary>
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
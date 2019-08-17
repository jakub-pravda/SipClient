using Javor.SipSerializer.Attributes;
using Javor.SipSerializer.Schemes;
using System;
using System.Text;

namespace Javor.SipSerializer.HeaderFields
{
    /// <summary>
    ///     Identification (from / to) header field
    /// </summary>
    public class Identification
    {
        /// <summary>
        ///     Initialize new identification (from / to) header
        /// </summary>
        /// <param name="sipUri">From/To identification</param>
        /// <param name="tag">Tag id</param>
        /// <param name="displayName">Display name parameter</param>
        public Identification(SipUri sipUri, string tag, string displayName = null)
        {
            Uri = sipUri ?? throw new ArgumentNullException("Invalid client URI.");
            DisplayName = displayName;
            Tag = tag;
        }

        /// <summary>
        ///     Initialize new identification (from/to) header
        /// </summary>
        /// <param name="sipUri">From/To identification</param>
        /// <param name="tag">Tag id</param>
        /// <param name="displayName">Display name parameter</param>
        public Identification(string sipUri, string tag, string displayName = null)
            : this(SipUri.Parse(sipUri), tag, displayName)
        {
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

        private const string _tagId = "tag=";

        /// <summary>
        ///     Create new identification tag
        /// </summary>
        /// <param name="s">Full raw identification line</param>
        /// <returns>New identification</returns>
        public static Identification Parse(string s)
        {
            ReadOnlySpan<char> roSpan = s.AsSpan();

            ReadOnlySpan<char> displayName = null; // value is nullable
            ReadOnlySpan<char> uri;
            ReadOnlySpan<char> tag;

            // display name minning
            if (roSpan[0] == '"')
            {
                int i;
                for (i = 1; i < roSpan.Length - 1; i++)
                    if (roSpan[i] == '"')
                        break;

                displayName = roSpan.Slice(1, i - 1);
            }

            // sip uri name minning
            int uriStartIndex = roSpan.IndexOf('<');
            int uriStopIndex = roSpan.IndexOf('>');
            uri = roSpan.Slice(uriStartIndex + 1, uriStopIndex - uriStartIndex - 1);

            // tag minning
            int tagIndex = roSpan.IndexOf(_tagId.AsSpan()) + _tagId.Length;
            tag = roSpan.Slice(tagIndex, roSpan.Length - tagIndex);

            return new Identification(uri.ToString(), tag.ToString(), displayName.ToString());
        }
    }
}
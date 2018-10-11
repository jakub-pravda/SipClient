using System.Text;
using Javor.SdpSerializer.Attributes;
using Javor.SdpSerializer.Exceptions;
using Javor.SdpSerializer.Helpers;

namespace Javor.SdpSerializer.Specifications
{
    /// <summary>
    ///     Attribute description.
    /// </summary>
    [DescriptionField('a')]
    public class  SessionAttribute : SdpField
    {
        /// <summary>
        ///     Attribute name. Valid only for value attributes.
        /// </summary>
        public string AttributeName { get; private set; }
        
        /// <summary>
        ///     Attribute value. Valid for both value attributes and binary attributes.
        /// </summary>
        public string AttributeValue { get; private set; }
        
        /// <summary>
        ///     Initialize new binary attribute.
        /// </summary>
        /// <param name="flag">Attribute flag.</param>
        public SessionAttribute(string flag)
        {
            AttributeValue = flag;
        }

        /// <summary>
        ///     Initialize new value attribute.
        /// </summary>
        /// <param name="attribute">Attribute name.</param>
        /// <param name="value">Attribute value.</param>
        public SessionAttribute(string attribute, string value)
        {
            AttributeName = attribute;
            AttributeValue = value;
        }

        /// <inheritdoc />
        public override string Encode()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.Append(SdpType);
            
            if (string.IsNullOrEmpty(AttributeName))
            {
                // return flag attribute
                sb.Append(AttributeValue);
            }
            else
            {
                // return value attribute
                sb.Append(AttributeName);
                sb.Append(":");
                sb.Append(AttributeValue);
            }
            
            return sb.ToString();
        }
    }
}
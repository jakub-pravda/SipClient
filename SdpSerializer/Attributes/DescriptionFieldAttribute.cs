using System;
using Javor.SdpSerializer.Exceptions;

namespace Javor.SdpSerializer.Attributes
{
    /// <summary>
    ///     Description field attribute.
    /// </summary>
    public class DescriptionFieldAttribute : Attribute
    {
        /// <summary>
        ///     Initialize new description field attribute.
        /// </summary>
        /// <param name="descriptionField">SDP description field attribute.</param>
        /// <exception cref="SdpFormatException">Invalid description field.</exception>
        public DescriptionFieldAttribute(char descriptionField)
        {
            if (!char.IsLetter(descriptionField))
                throw new SdpFormatException("Invalid description field value. Must be single letter.");

            _descriptionField = descriptionField.ToString(); // TODO performance test
        }

        private string _descriptionField;

        /// <summary>
        ///     Get description field.
        /// </summary>
        /// <returns>SDP description field attribute.</returns>
        public string GetDescriptionField()
        {
            return _descriptionField;
        }
    }
}
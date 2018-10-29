using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Javor.SipSerializer.Attributes;

namespace Javor.SipSerializer.Extensions
{
    /// <summary>
    ///     Object extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     This extension checks if model has all required fields.
        ///      Required field in model is marked with Required attribute.
        /// </summary>
        /// <param name="toValidate">Model which should be validated.</param>
        /// <returns>True if model has all required fields. False otherwise.</returns>
        public static bool Validate(this object toValidate)
        {
            if (toValidate == null) throw new ArgumentNullException("Model for validation cann't be null.");

            PropertyInfo[] properties = toValidate.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                RequiredAttribute required = property.GetCustomAttribute<RequiredAttribute>();

                if (required != null && property.GetValue(toValidate) == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

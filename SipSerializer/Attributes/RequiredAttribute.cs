using System;
using System.Collections.Generic;
using System.Text;

namespace Javor.SipSerializer.Attributes
{
    public class RequiredAttribute : Attribute
    {
        public RequiredAttribute()
        {
        }

        public RequiredAttribute(string errorMessage)
        {
            _errorMessage = errorMessage;
        }

        private string _errorMessage;

        public string GetErrorMessage()
        {
            return _errorMessage;
        }
    }
}

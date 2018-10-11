using System;

namespace Javor.SipSerializer.Attributes
{
    public class ParameterNameAttribute : Attribute
    {
        public ParameterNameAttribute(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Parametr name cann't be null.");
            }

            _name = name;
        }

        private string _name;

        public string GetParameterName()
        {
            return _name;
        }
    }
}
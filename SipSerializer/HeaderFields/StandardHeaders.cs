using Javor.SipSerializer.Attributes;
using Javor.SipSerializer.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Javor.SipSerializer.HeaderFields
{
    /// <summary>
    ///     Default sip headers valid for all types of sip messages.
    /// </summary>
    public class StandardHeaders
    {
        [HeaderName(HeaderFieldsNames.From)]
        public virtual Identification From { get; set; }
         
        [HeaderName(HeaderFieldsNames.To)]
        public virtual Identification To { get; set; }
        
        [HeaderName(HeaderFieldsNames.CallId)]
        public virtual string CallId { get; set; }
        
        [HeaderName(HeaderFieldsNames.MaxForwards)]
        public virtual string MaxForwards { get; set; }

        [HeaderName(HeaderFieldsNames.Via)]
        public virtual ICollection<Via> Via { get; set; }
        
        [HeaderName(HeaderFieldsNames.Cseq)]
        public virtual CSeq CSeq { get; set; }

        [HeaderName(HeaderFieldsNames.Contact, HeaderFieldsNames.ContactCompactForm)]
        public virtual string Contact { get; set; }
        
        [HeaderName(HeaderFieldsNames.Require)]
        public virtual IEnumerable<string> Require { get; set; }

        [HeaderName(HeaderFieldsNames.Supported)]
        public virtual string Supported { get; set; }
        
        [HeaderName(HeaderFieldsNames.Unsupported)]

        public virtual string Unsupported { get; set; }

        [HeaderName(HeaderFieldsNames.ContentLength)]
        public virtual int ContentLength { get; set; }

        public virtual IDictionary<string, string> UnknownHeaders { get; set; }
        
        /// <summary>
        ///     Convert SIP headers into the string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] properties = typeof(StandardHeaders)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(this);
                if (value != null)
                {
                    HeaderNameAttribute headerName =
                        (HeaderNameAttribute)property.GetCustomAttribute(typeof(HeaderNameAttribute));

                    if (property.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                    {
                        // deal with other enums (string inherits from enum, for that reason is 
                        //  string type control first and other enum type control 
                        //  (which using foreach loop) second)
                        foreach (var item in (IEnumerable)value)
                        {
                            sb.AppendSipLine(headerName.GetHeaderFullName(), item.ToString());
                        }
                    }
                    else
                    {
                        // deal with other data types
                        sb.AppendSipLine(headerName.GetHeaderFullName(), value.ToString());
                    }
                }
            }

            return sb.ToString();
        }
    }
}
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
        [HeaderName(HeaderName.From)]
        public virtual Identification From { get; set; }
         
        [HeaderName(HeaderName.To)]
        public virtual Identification To { get; set; }
        
        [HeaderName(HeaderName.CallId)]
        public virtual string CallId { get; set; }
        
        [HeaderName(HeaderName.MaxForwards)]
        public virtual string MaxForwards { get; set; }

        [HeaderName(HeaderName.Via)]
        public virtual ICollection<Via> Via { get; set; }
        
        [HeaderName(HeaderName.Cseq)]
        public virtual CSeq CSeq { get; set; }

        [HeaderName(HeaderName.Contact, HeaderName.ContactCompactForm)]
        public virtual string Contact { get; set; }
        
        [HeaderName(HeaderName.Require)]
        public virtual IEnumerable<string> Require { get; set; }

        [HeaderName(HeaderName.Supported)]
        public virtual string Supported { get; set; }
        
        [HeaderName(HeaderName.Unsupported)]

        public virtual string Unsupported { get; set; }

        [HeaderName(HeaderName.ContentLength)]
        public virtual int ContentLength { get; set; }
        
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
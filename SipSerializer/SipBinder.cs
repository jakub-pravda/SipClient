using Javor.SipSerializer.HeaderFields;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Javor.SipSerializer
{
    public class SipBinder : Binder
    {
        // TODO inherit from defautl implementation of binder?
        public override FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] names, out object state)
        {
            throw new NotImplementedException();
        }

        public override object ChangeType(object value, Type type, CultureInfo culture)
        {
            Type oldOneType = value.GetType();

            if (Type.GetTypeCode(oldOneType) == TypeCode.String)
            {
                // to primitive conversion...
                if (type.IsPrimitive)
                {
                    TypeCode newOneTypeCode = Type.GetTypeCode(type);

                    switch (newOneTypeCode)
                    {
                        case (TypeCode.Int32):
                            return Convert.ToInt32((string)value);

                        default:
                            return null;
                    }
                }
                // to collection string conversion...
                else if (type == typeof(IEnumerable<string>))
                {
                    return ((string)value).Split(',')
                        .Select(p => p.Trim())
                        .ToList(); // TODO ',' to ABNF?
                }
                else if (type == typeof(ICollection<Via>))
                {
                    return new List<Via>() { new Via((string)value) };
                }
            }

            return null; // TODO unsupported binding
        }

        public override void ReorderArgumentArray(ref object[] args, object state)
        {
            throw new NotImplementedException();
        }

        public override MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public override PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }
    }
}

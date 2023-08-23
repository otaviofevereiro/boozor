using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Boozor.Components.Fields
{
    public class SelectEnumField<TValue> : SelectFieldBKP<TValue>
    {
        protected override IEnumerable<KeyValuePair<object, string>> GetItens()
        {
            var type = typeof(TValue);

            foreach (var value in Enum.GetValues(typeof(TValue)))
            {
                var memberInfo = type.GetMember(type.GetEnumName(value));
                var descriptionAttribute = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false)
                                                        .FirstOrDefault() as DescriptionAttribute;

                if (descriptionAttribute != null)
                    yield return new KeyValuePair<object, string>(value, descriptionAttribute.Description);
                else
                    yield return new KeyValuePair<object, string>(value, Enum.GetName(type, value));
            }
        }
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Curriculum.Client.Shared
{
    public class CustomSelect<TEnum> : InputSelect<TEnum>
        where TEnum : IConvertible
    {
        protected override bool TryParseValueFromString(string value, out TEnum result, out string validationErrorMessage)
        {
            return base.TryParseValueFromString(value, out result, out validationErrorMessage);
        }

    }

    public partial class InputEnumSelect<TEnum> 
        where TEnum : IConvertible
    {
        private IReadOnlyCollection<KeyValuePair<int, string>> itens;

        [Parameter]
        public TEnum Value { get; set; }

        [Parameter]
        public EventCallback<TEnum> ValueChanged { get; set; }

        protected override void OnInitialized()
        {
            itens = GetItens().ToList();

            base.OnInitialized();
        }

        private IEnumerable<KeyValuePair<int, string>> GetItens()
        {
            var type = typeof(TEnum);

            foreach (var value in Enum.GetValues(typeof(TEnum)))
            {
                var memberInfo = type.GetMember(type.GetEnumName(value));
                var descriptionAttribute = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false)
                                                        .FirstOrDefault() as DescriptionAttribute;

                if (descriptionAttribute != null)
                    yield return new KeyValuePair<int, string>(((TEnum)value).ToInt32(CultureInfo.InvariantCulture), descriptionAttribute.Description);
                else
                    yield return new KeyValuePair<int, string>(((TEnum)value).ToInt32(CultureInfo.InvariantCulture), Enum.GetName(type, value));
            }
        }
    }
}

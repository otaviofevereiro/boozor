using Curriculum.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace Curriculum.Client.Shared
{
    public partial class InputEnumSelect<TEnum>
        where TEnum : IConvertible
    {
        private IReadOnlyCollection<KeyValuePair<object, string>> itens;

        [Parameter]
        public TEnum Value { get; set; }

        [Parameter]
        public EventCallback<TEnum> ValueChanged { get; set; }

        protected override void OnInitialized()
        {
            itens = GetItens().ToList();

            base.OnInitialized();
        }

        private IEnumerable<KeyValuePair<object, string>> GetItens()
        {
            var type = typeof(TEnum);

            foreach (var value in Enum.GetValues(typeof(TEnum)))
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

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Boozor.Components.Fields
{
    public abstract class SelectField<TValue> : Field<TValue>
    {
        private readonly Type _nullableUnderlyingType = Nullable.GetUnderlyingType(typeof(TValue));
        private IReadOnlyCollection<KeyValuePair<object, string>> itens;

        protected override void BuildInput(RenderTreeBuilder builder, int sequence)
        {
            builder.OpenElement(0, "select");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", $"form-control {CssClass}");
            builder.AddAttribute(3, "value", BindConverter.FormatValue(CurrentValueAsString));
            builder.AddAttribute(4, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));

            RenderOptions(builder);

            builder.CloseElement();
        }
        protected abstract IEnumerable<KeyValuePair<object, string>> GetItens();

        protected override void OnInitialized()
        {
            itens = GetItens().ToList();

            base.OnInitialized();
        }
        protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
        {
            if (typeof(TValue) == typeof(string))
            {
                result = (TValue)(object)value;
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(TValue).IsEnum || (_nullableUnderlyingType != null && _nullableUnderlyingType.IsEnum))
            {
                var success = BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out var parsedValue);
                if (success)
                {
                    result = parsedValue;
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    result = default;
                    validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";
                    return false;
                }
            }

            throw new InvalidOperationException($"{GetType()} does not support the type '{typeof(TValue)}'.");
        }

        private void RenderOptions(RenderTreeBuilder builder)
        {
            int sequence = -1;

            foreach (var item in itens)
            {
                builder.OpenElement(0, "option");
                builder.AddAttribute(sequence++, "value", item.Key);
                builder.AddContent(0, item.Value);
                builder.CloseElement();
            }
        }
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;

namespace Boozor.Components.Fields
{
    public class CheckboxField : FieldBKP<bool>
    {
        /// <inheritdoc />
        protected override void BuildInput(RenderTreeBuilder builder, int sequence)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "form-check");
            {
                builder.OpenElement(0, "input");
                builder.AddMultipleAttributes(1, AdditionalAttributes);
                builder.AddAttribute(2, "type", "checkbox");
                builder.AddAttribute(3, "class", $"form-check-input {CssClass}");
                builder.AddAttribute(4, "checked", BindConverter.FormatValue(CurrentValue));
                builder.AddAttribute(5, "onchange", EventCallback.Factory.CreateBinder<bool>(this, __value => CurrentValue = __value, CurrentValue));
                builder.CloseElement();

                object placeholder = null;
                AdditionalAttributes?.TryGetValue("placeholder", out placeholder);

                builder.OpenElement(0, "label");
                builder.AddAttribute(1, "class", "form-check-label");
                builder.AddContent(0, placeholder ?? Label);
                builder.CloseElement();

            }
            builder.CloseElement();
        }

        /// <inheritdoc />
        protected override bool TryParseValueFromString(string value, out bool result, out string validationErrorMessage)
        {
            throw new NotImplementedException($"This component does not parse string inputs. Bind to the '{nameof(CurrentValue)}' property, not '{nameof(CurrentValueAsString)}'.");
        }

    }
}

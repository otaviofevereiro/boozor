using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Boozor.Components.Fields
{
    public class TextAreaField : Field<string>
    {
        protected override void BuildInput(RenderTreeBuilder builder, int sequence)
        {
            builder.OpenElement(0, "textarea");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", $"form-control {CssClass}");
            builder.AddAttribute(3, "value", BindConverter.FormatValue(CurrentValue));
            builder.AddAttribute(4, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));
            builder.CloseElement();
        }

        /// <inheritdoc />
        protected override bool TryParseValueFromString(string value, out string result, out string validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }
    }
}

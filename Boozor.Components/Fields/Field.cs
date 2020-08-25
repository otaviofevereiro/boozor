using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System.Linq;

namespace Boozor.Components.Fields
{
    public abstract class Field<TValue> : InputBase<TValue>
    {
        [Parameter] public string Label { get; set; }

        protected abstract void BuildInput(RenderTreeBuilder builder, int sequence);

        protected override void OnInitialized()
        {
            EnsureLabel();

            base.OnInitialized();
        }

        protected sealed override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "form-group row");
            {
                BuildLabel(builder);

                builder.OpenElement(0, "div");
                builder.AddAttribute(0, "class", "col-10");
                {
                    BuildInput(builder, 0);

                    builder.OpenComponent<ValidationMessage<TValue>>(0);
                    builder.AddAttribute(1, "For", ValueExpression);
                    builder.CloseComponent();
                }
                builder.CloseElement();
            }
            builder.CloseElement();
        }

        private void BuildLabel(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "label");
            builder.AddAttribute(1, "class", "col-2 col-form-label");
            builder.AddContent(0, Label);
            builder.CloseElement();
        }

        private void EnsureLabel()
        {
            if (!string.IsNullOrEmpty(Label))
                return;

            var modelType = EditContext.Model.GetType();
            var memberExpression = (System.Linq.Expressions.MemberExpression)ValueExpression.Body;
            var property = modelType.GetProperty(memberExpression.Member.Name);
            var displayAttribute = property.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), true)
                                           .FirstOrDefault() as System.ComponentModel.DataAnnotations.DisplayAttribute;

            if (displayAttribute == null || string.IsNullOrEmpty(displayAttribute.Name))
                Label = memberExpression.Member.Name;
            else
                Label = displayAttribute.Name;
        }
    }
}

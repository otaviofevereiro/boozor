using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace Curriculum.Client.Shared
{
    public class Column<TValue> : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

        [Parameter]
        public string Title { get; set; }

        [CascadingParameter]
        public Type ModelType { get; set; }

        [CascadingParameter]
        public object Item { get; set; }

        [Parameter]
        public Expression<Func<TValue>> ValueExpression { get; set; }

        private void EnsureTitle()
        {
            if (Item == null)
            {
                if (!string.IsNullOrEmpty(Title))
                    return;

                var memberExpression = (MemberExpression)ValueExpression.Body;
                var property = ModelType.GetProperty(memberExpression.Member.Name);

                var displayAttribute = property?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), true)
                                               ?.FirstOrDefault() as System.ComponentModel.DataAnnotations.DisplayAttribute;

                if (displayAttribute == null || string.IsNullOrEmpty(displayAttribute.Name))
                    Title = memberExpression.Member.Name;
                else
                    Title = displayAttribute.Name;
            }
            else
            {
                var memberExpression = (MemberExpression)ValueExpression.Body;
                var property = Item.GetType().GetProperty(memberExpression.Member.Name);
                Title = property.GetValue(Item).ToString();
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            EnsureTitle();
            builder.OpenElement(0, "th");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddContent(2, Title);
            builder.CloseElement();
        }
    }

    public class DataTable<TModel> : ComponentBase //, IDisposable
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public IEnumerable<TModel> Itens { get; set; }


        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "table");
            builder.AddAttribute(1, "class", "table");
            {
                builder.OpenElement(2, "thead");
                {
                    builder.OpenElement(3, "tr");
                    {
                        builder.OpenComponent<CascadingValue<Type>>(4);
                        builder.AddAttribute(5, "Value", typeof(TModel));
                        //builder.AddAttribute(6, "Name", "ModelType");
                        builder.AddAttribute(7, "ChildContent", ChildContent);
                        builder.AddAttribute(8, "ISFixed", true);
                        builder.CloseComponent();
                    }
                    builder.CloseElement();
                }
                builder.CloseElement();

                builder.OpenElement(8, "tbody");
                {
                    foreach (var item in Itens)
                    {
                        builder.OpenElement(9, "tr");
                        {
                            builder.OpenComponent<CascadingValue<TModel>>(10);
                            builder.AddAttribute(5, "Name", "Item");
                            builder.AddAttribute(6, "Value", item);
                            builder.AddAttribute(7, "ChildContent", ChildContent);
                            builder.CloseComponent();
                        }
                        builder.CloseElement();
                    }
                }
                builder.CloseElement();

            }
            builder.CloseElement();
        }
    }
}

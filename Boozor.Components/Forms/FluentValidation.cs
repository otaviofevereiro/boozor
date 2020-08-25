using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace Boozor.Components.Forms
{
    public class FluentValidation : ComponentBase
    {
        [CascadingParameter] EditContext EditContext { get; set; }

        protected override void OnInitialized()
        {
            if (EditContext == null)
            {
                throw new InvalidOperationException($"{nameof(FluentValidation)} requires a cascading " +
                                                    $"parameter of type {nameof(EditContext)}. For example, you can use {nameof(FluentValidation)} " +
                                                    $"inside an {nameof(EditForm)}.");
            }

            EditContext.AddFluentValidation();
        }
    }
}

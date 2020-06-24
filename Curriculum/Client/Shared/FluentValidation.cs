using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace Curriculum.Client.Shared
{
    public class FluentValidation : ComponentBase
    {
        [CascadingParameter] EditContext CurrentEditContext { get; set; }

        protected override void OnInitialized()
        {
            if (CurrentEditContext == null)
            {
                throw new InvalidOperationException($"{nameof(FluentValidation)} requires a cascading " +
                                                    $"parameter of type {nameof(EditContext)}. For example, you can use {nameof(FluentValidation)} " +
                                                    $"inside an {nameof(EditForm)}.");
            }

            CurrentEditContext.AddFluentValidation();
        }
    }
}

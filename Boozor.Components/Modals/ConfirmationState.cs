using System;
using System.Threading.Tasks;

namespace Boozor.Components.Modals
{
    public record ConfirmationState : MessageState
    {
        public string OkText { get; init; }
        public string CancelText { get; init; }

        public event EventHandler<MessageResult> OnOk;
        public event EventHandler<MessageResult> OnCancel;

        public void Ok()
        {
            OnOk?.Invoke(this, MessageResult.Ok);
        }

        public void Cancel()
        {
            OnCancel?.Invoke(this, MessageResult.Cancel);
        }
    }
}

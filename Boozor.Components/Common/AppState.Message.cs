using Boozor.Components.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boozor.Components.Common
{
    public partial class AppState
    {
        private MessageState _messageState;

        public event Action<MessageState> MessageChange;

        public void Show(MessageState messageState)
        {
            MessageChange?.Invoke(_messageState);
        }

        public void Confirmation(string text, string title, Action onOk = default, Action onCancel = default)
        {
            MessageState state = new()
            {
                Title = title,
                Text = text
            };

            state.AddButton("Cancelar", onCancel, "light")
                 .AddButton("Ok", onOk, "primary");
                 
            MessageChange?.Invoke(state);
        }
    }
}

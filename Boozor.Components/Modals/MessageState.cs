using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boozor.Components.Modals
{
    public record MessageState
    {
        private readonly List<MessageButton> _buttons = new();

        public string Text { get; init; }
        public string Title { get; init; }

        public IReadOnlyList<MessageButton> Buttons => _buttons;

        public MessageState AddButton(string text, Action action, string color)
        {
            _buttons.Add(new MessageButton(text, action, color));

            return this;
        }
    }
}

using System;
using System.Threading.Tasks;

namespace Boozor.Components.Modals
{
    public class MessageButton
    {
        public MessageButton(string text, Action action, string color)
        {
            Text = text;
            Action = action;
            Color = color;
        }

        public string Text { get; }
        public Action Action { get; }
        public string Color { get; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boozor.Components.Modals
{
    public abstract record MessageState
    {
        public string Text { get; init; }
        public string Title { get; init; }
    }
}

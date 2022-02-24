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
        private ConfirmationState _confirmationState;

        public event Action<ConfirmationState> ConfimationChange;

        public void Show(ConfirmationState confirmationState)
        {
            ConfimationChange?.Invoke(_confirmationState);
        }
    }
}

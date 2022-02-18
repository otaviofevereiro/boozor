using System;

namespace Boozor.Components.Common
{
    public class AppState
    {
        private bool _loading;

        public event Action<bool> LoadingChanged;

        public void Loading()
        {
            _loading = !_loading;

            LoadingChanged?.Invoke(_loading);
        }
    }
}

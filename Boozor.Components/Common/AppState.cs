using System;

namespace Boozor.Components.Common
{
    public class AppState
    {
        private bool _loading;

        public event Action<bool> LoadingChanged;

        public void SetLoading(bool loading)
        {
            if (_loading != loading)
                _loading = loading;

            LoadingChanged?.Invoke(_loading);
        }
    }
}

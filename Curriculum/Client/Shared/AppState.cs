using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curriculum.Client.Shared
{
    public class AppState
    {
        public bool loading;

        public event Action<bool> LoadingChanged;

        public void SetLoading(bool loading)
        {
            if (this.loading != loading)
                this.loading = loading;

            LoadingChanged?.Invoke(this.loading);
        }
    }
}

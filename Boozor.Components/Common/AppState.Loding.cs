﻿using Boozor.Components.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boozor.Components.Common
{
    public partial class AppState
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
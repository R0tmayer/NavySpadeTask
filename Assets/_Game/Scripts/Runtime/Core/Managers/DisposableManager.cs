﻿using System;
using System.Collections.Generic;

namespace NavySpade.Core.Managers
{
    public class DisposableManager
    {
        private readonly IEnumerable<IDisposable> _disposables;

        public DisposableManager(params IDisposable[] disposables)
        {
            _disposables = disposables;
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
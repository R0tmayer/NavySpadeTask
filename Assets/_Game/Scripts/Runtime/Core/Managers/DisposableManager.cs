using System;
using System.Collections.Generic;

namespace NavySpade.Core.Managers
{
    public class DisposableManager
    {
        private readonly IDisposable[] _disposables;

        public DisposableManager(params IDisposable[] disposables)
        {
            _disposables = disposables;
        }

        public void Dispose()
        {
            for (int i = 0; i < _disposables.Length; i++)
            {
                _disposables[i].Dispose();
            }
        }
    }
}
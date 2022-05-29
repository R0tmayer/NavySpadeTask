
using System;
using System.Collections.Generic;

namespace NavySpade.Core.Root
{
    public class DisposableManager
    {
        private IEnumerable<IDisposable> _disposables;

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
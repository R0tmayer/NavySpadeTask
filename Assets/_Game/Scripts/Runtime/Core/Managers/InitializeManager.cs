using System.Collections.Generic;
using NavySpade.Core.Interfaces;

namespace NavySpade.Core.Managers
{
    public class InitializeManager
    {
        private IEnumerable<IInitializable> _initializables;

        public InitializeManager(params IInitializable[] initializables)
        {
            _initializables = initializables;
        }

        public void Initialize()
        {
            foreach (var initializable in _initializables)
            {
                initializable.Initialize();
            }
        }
    }
}
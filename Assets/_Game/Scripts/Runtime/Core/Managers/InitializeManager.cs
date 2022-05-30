using System.Collections.Generic;
using NavySpade.Core.Interfaces;

namespace NavySpade.Core.Managers
{
    public class InitializeManager
    {
        private readonly IInitializable[] _initializables;

        public InitializeManager(params IInitializable[] initializables)
        {
            _initializables = initializables;
        }

        public void Initialize()
        {
            for (int i = 0; i < _initializables.Length; i++)
            {
                _initializables[i].Initialize();
            }
        }
    }
}
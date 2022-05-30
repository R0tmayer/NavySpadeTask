using System.Collections.Generic;
using NavySpade.Core.Interfaces;

namespace NavySpade.Core.Managers
{
    public class TickableManager
    {
        private readonly ITickable[] _tickables;

        public TickableManager(params ITickable[] tickables)
        {
            _tickables = tickables;
        }

        public void Tick()
        {
            for (int i = 0; i < _tickables.Length; i++)
            {
                _tickables[i].Tick();
            }
        }
    }
}
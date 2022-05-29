using System.Collections.Generic;
using NavySpade.Core.Interfaces;

namespace NavySpade.Core.Managers
{
    public class TickableManager
    {
        private IEnumerable<ITickable> _tickables;

        public TickableManager(params ITickable[] tickables)
        {
            _tickables = tickables;
        }

        public void Tick()
        {
            foreach (var initializable in _tickables)
            {
                initializable.Tick();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using NavySpade.Core.Interfaces;

namespace NavySpade.Core.Scores
{
    public class SaveSystem : IInitializable, IDisposable
    {
        private readonly IEnumerable<ISavable> _savables;

        public SaveSystem(params ISavable[] savables)
        {
            _savables = savables;
        }

        private void Load()
        {
            foreach (var savable in _savables)
            {
                savable.Load();
            }
        }

        private void Save()
        {
            foreach (var savable in _savables)
            {
                savable.Save();
            }
        }

        public void Initialize() => Load();
        public void Dispose() => Save();
    }
}
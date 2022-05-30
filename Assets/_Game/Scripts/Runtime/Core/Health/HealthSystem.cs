
using System;
using NavySpade.Core.Interfaces;
using NavySpade.Core.PlayerInfrastructure;

namespace NavySpade.Core.Health
{
    public class HealthSystem : IInitializable, IDisposable
    {
        private readonly Player _player;

        public HealthSystem(Player player)
        {
            _player = player;
        }

        public void Initialize()
        {
            _player.ItemCollected += _player.HealthComponent.IncreaseValue;
        }

        public void Dispose()
        {
            _player.ItemCollected -= _player.HealthComponent.IncreaseValue;
        }
    }
}
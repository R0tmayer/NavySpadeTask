
using System;
using NavySpade.Core.Interfaces;
using NavySpade.Core.Managers;
using NavySpade.Core.PlayerInfrastructure;

namespace NavySpade.Core.Health
{
    public class HealthSystem : IInitializable, IDisposable
    {
        private readonly Player _player;
        private readonly Mediator _mediator;

        public HealthSystem(Player player, Mediator mediator)
        {
            _player = player;
            _mediator = mediator;
        }

        public void Initialize()
        {
            _player.ItemCollected += _player.HealthComponent.IncreaseValue;
            _player.HealthComponent.Died += _mediator.ShowEndGamePanel;
        }

        public void Dispose()
        {
            _player.ItemCollected -= _player.HealthComponent.IncreaseValue;
            _player.HealthComponent.Died -= _mediator.ShowEndGamePanel;
        }
    }
}
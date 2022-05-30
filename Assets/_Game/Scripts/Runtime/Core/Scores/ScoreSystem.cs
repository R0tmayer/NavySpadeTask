using System;
using NavySpade.Core.Interfaces;
using NavySpade.Core.PlayerInfrastructure;

namespace NavySpade.Core.Scores
{
    public class ScoreSystem : IInitializable, IDisposable
    {
        private readonly Player _player;
        private readonly Score _score;

        public ScoreSystem(Player player, Score score)
        {
            _score = score;
            _player = player;
        }

        public void Initialize()
        {
            _player.ItemCollected += _score.IncreaseValue;
        }

        public void Dispose()
        {
            _player.ItemCollected -= _score.IncreaseValue;
        }
    }
}
using UnityEngine;

namespace NavySpade.Core.Configs
{
    [CreateAssetMenu(menuName = "Configs/Player Config", fileName = "Player Config", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private int _moveSpeed;
        [SerializeField] private int _pauseCollisionTime;
        [SerializeField] private int _health;

        public int MoveSpeed => _moveSpeed;
        public int Health => _health;

        public int PauseCollisionTime => _pauseCollisionTime;
    }
}

using UnityEngine;

namespace NavySpade.Core.Configs
{
    [CreateAssetMenu(menuName = "Configs/Enemy Config", fileName = "Enemy Config", order = 0)]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private int _moveSpeed;
        [SerializeField] private int _movePeriod;

        public int MoveSpeed => _moveSpeed;
        public int MovePeriod => _movePeriod;
    }
}
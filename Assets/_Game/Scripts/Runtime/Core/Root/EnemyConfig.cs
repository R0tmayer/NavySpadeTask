
using UnityEngine;

namespace NavySpade.Core.Root
{
    [CreateAssetMenu(menuName = "Configs/Enemy Config", fileName = "Enemy Config", order = 0)]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private int _moveSpeed;

        public int MoveSpeed => _moveSpeed;
    }
}
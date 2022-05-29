using UnityEngine;

namespace NavySpade.Core.Configs
{
    [CreateAssetMenu(menuName = "Configs/Player Config", fileName = "Player Config", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private int _moveSpeed;
        [SerializeField] private float _health;

        public int MoveSpeed => _moveSpeed;
        public float Health => _health;

        
    }
}

using UnityEngine;

namespace NavySpade.Core.Root
{
    public class EnemyFactory 
    {
        private readonly GameObject _prefab;
        private readonly Transform _container;

        public EnemyFactory(GameObject prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
        }

        public GameObject GetNewInstance() => Object.Instantiate(_prefab, _container, true);
    }
}
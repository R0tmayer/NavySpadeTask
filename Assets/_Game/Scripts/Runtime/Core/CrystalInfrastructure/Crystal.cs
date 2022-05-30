using NavySpade.Core.Interfaces;
using UnityEngine;

namespace NavySpade.Core.CrystalInfrastructure
{
    public class Crystal : MonoBehaviour, ICollectable
    {
        public void Collect()
        {
            gameObject.SetActive(false);
        }
    }
}
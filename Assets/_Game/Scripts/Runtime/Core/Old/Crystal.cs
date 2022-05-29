using NavySpade.Core.Interfaces;
using UnityEngine;

namespace NavySpade.Core.Old
{
    public class Crystal : MonoBehaviour, ICollectable
    {
        public void Collect()
        {
            gameObject.SetActive(false);
        }
    }
}
using System.Collections;
using UnityEngine;

namespace NavySpade.Core.Interfaces
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator routine);
    }
}
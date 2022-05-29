using System.Collections;
using UnityEngine;

namespace NavySpade.Core.Root
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator routine);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamecore
{
    public static partial class CoroutineUtility
    {
        public static void StartCoroutineLoop(this GameObject key, IEnumerator routine)
        {
            var _coroutine = ExecuteCoroutine(LoopCoroutine(key, routine));

            if (!coroutines.ContainsKey(key))
            {
                coroutines[key] = new List<Coroutine>();
            }

            coroutines[key].Add(_coroutine);
        }
        public static void StartCoroutineLoop(this GameObject key, float startDelay, IEnumerator routine)
        {
            var _coroutine = ExecuteCoroutine(StartCoroutineDelayed(startDelay, LoopCoroutine(key, routine))); 
            TryAddCoroutine(key, _coroutine);
        }
        private static IEnumerator LoopCoroutine(GameObject key, IEnumerator routine)
        {
            while (true)
            {
                if (key == null)
                {
                    coroutines.Remove(key);
                    yield break;
                }

                yield return routine;
            }
        }
    }
}

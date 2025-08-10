using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamecore
{
    public static partial class CoroutineUtility
    {
        public static void StartCoroutineLoop(this GameObject key, Coroutine routine)
        {
            var _coroutine = Game.Manager.StartCoroutine(LoopCoroutine(key, routine));

            if (!coroutines.ContainsKey(key))
            {
                coroutines[key] = new List<Coroutine>();
            }

            coroutines[key].Add(_coroutine);
        }
        private static IEnumerator LoopCoroutine(GameObject key, Coroutine routine)
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamecore
{
    public static partial class CoroutineUtility
    {
        public static void StartCoroutineLoop(this GameObject key, float time, Action callBack)
        {
            var _coroutine = Game.Manager.StartCoroutine(LoopCoroutine(key, time, callBack));

            if (!coroutines.ContainsKey(key))
            {
                coroutines[key] = new List<Coroutine>();
            }

            coroutines[key].Add(_coroutine);
        }
        private static IEnumerator LoopCoroutine(GameObject key, float time, Action callBack)
        {
            while (true)
            {
                if (key == null)
                {
                    coroutines.Remove(key);
                    yield break;
                }
            
                callBack?.Invoke();
                yield return new WaitForSeconds(time);
            }
        }
    }
}

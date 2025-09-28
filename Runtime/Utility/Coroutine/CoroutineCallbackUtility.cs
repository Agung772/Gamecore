using System;
using System.Collections;
using UnityEngine;

namespace Gamecore
{
    public static partial class CoroutineUtility
    {
        public static void StartCoroutine(this GameObject key, float startDelay, Action callBack)
        {
            var _coroutine = ExecuteCoroutine(key, CallBackCoroutine(key, startDelay, callBack)); 
            TryAddCoroutine(key, _coroutine);
        }
        private static IEnumerator CallBackCoroutine(GameObject key, float startDelay, Action callBack)
        {
            yield return new WaitForSeconds(startDelay);
            if (key == null)
            {
                Coroutines.Remove(key);
                yield break;
            }
            
            callBack?.Invoke();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamecore
{
    public static class CoroutineUtility
    {
        private static Dictionary<GameObject, List<Coroutine>> coroutines = new();
    
        public static void StartLoop(this GameObject key, float time, Action callBack)
        {
            var _coroutine = Game.Manager.StartCoroutine(LoopCoroutine(key, time, callBack));

            if (!coroutines.ContainsKey(key))
            {
                coroutines[key] = new List<Coroutine>();
            }

            coroutines[key].Add(_coroutine);
        }
        public static void StopLoop(this GameObject key)
        {
            if (!coroutines.ContainsKey(key)) return;

            foreach (var _coroutine in coroutines[key])
            {
                Game.Manager.StopCoroutine(_coroutine);
            }
            coroutines[key] = null;
            coroutines.Remove(key);
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


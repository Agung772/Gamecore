using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamecore
{
    public static partial class CoroutineUtility
    {
        private static Dictionary<GameObject, List<Coroutine>> coroutines = new();
        
        public static void StartCoroutine(this GameObject key, Coroutine routine)
        {
            var _coroutine = Game.Manager.StartCoroutine(Coroutine(routine));

            if (!coroutines.ContainsKey(key))
            {
                coroutines[key] = new List<Coroutine>();
            }

            coroutines[key].Add(_coroutine);
        }
        private static IEnumerator Coroutine(Coroutine routine)
        {
            yield return routine;
        }
        public static void StopCoroutine(this GameObject key)
        {
            if (!coroutines.ContainsKey(key)) return;

            foreach (var _coroutine in coroutines[key])
            {
                Game.Manager.StopCoroutine(_coroutine);
            }
            coroutines[key] = null;
            coroutines.Remove(key);
        }
        public static bool IsCoroutine(this GameObject key)
        {
            return coroutines.ContainsKey(key);
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamecore
{
    public static partial class CoroutineUtility
    {
        public static Dictionary<GameObject, List<Coroutine>> coroutines = new();

        private static void TryAddCoroutine(GameObject key, Coroutine routine)
        {
            if (!coroutines.ContainsKey(key))
            {
                coroutines[key] = new List<Coroutine>();
            }

            coroutines[key].Add(routine);
        }
        private static Coroutine ExecuteCoroutine(IEnumerator routine)
        {
            return Game.Manager.StartCoroutine(routine);
        }
        public static void StartCoroutine(this GameObject key, Func<IEnumerator> routineFunc)
        {
            var _coroutine = ExecuteCoroutine(routineFunc.Invoke());
            TryAddCoroutine(key, _coroutine);
        }
        public static void StartCoroutine(this GameObject key, float startDelay, Func<IEnumerator> routineFunc)
        {
            var _coroutine = ExecuteCoroutine(StartCoroutineDelayed(startDelay, routineFunc.Invoke()));
            TryAddCoroutine(key, _coroutine);
        }
        private static IEnumerator StartCoroutineDelayed(float startDelay, IEnumerator routine)
        {
            yield return new WaitForSeconds(startDelay);
            yield return routine;
        }
        public static void StopCoroutine(this GameObject key)
        {
            if (!coroutines.TryGetValue(key, out var _coroutines)) return;

            foreach (var _coroutine in _coroutines)
            {
                Game.Manager.StopCoroutine(_coroutine);
            }

            coroutines.Remove(key);
        }
        public static bool IsCoroutine(this GameObject key)
        {
            return coroutines.ContainsKey(key);
        }
    }
}


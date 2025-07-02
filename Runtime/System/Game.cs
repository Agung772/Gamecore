using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

namespace Gamecore
{
    public static class Game
    {
        public static GameManager Manager { get; internal set; }
        public static string CurrentScene { get; set; }
        private static Dictionary<Type, GlobalBehaviour> globals;
        private static Dictionary<Type, LocalBehaviour> locals;

        public static void Initialize()
        {
            CreateGlobal();
            locals = new Dictionary<Type, LocalBehaviour>();
            
            Get<SceneLoader>().OnLoad += LoadLocal;
            Get<SceneLoader>().OnUnlooad += UnloadLocal;
        }

        public static IEnumerator InitializeCoroutine()
        {
            foreach (var _global in globals.Values) { yield return _global.InitializeCoroutine(); }
        }
        
        private static void CreateGlobal()
        {
            globals = InstanceUtility.Create<GlobalBehaviour>();
            var _orderGlobal = OrderGlobal(globals.Values.ToArray());
            foreach (var _global in _orderGlobal) { _global.Initialize(); }
            foreach (var _global in _orderGlobal) { _global.PostInitialize(); }
        }

        private static GlobalBehaviour[] OrderGlobal(GlobalBehaviour[] global)
        {
            var _global = global.OrderBy(local => local.Order).ToArray();
            return _global;
        }

        private static void LoadLocal()
        {
            var _tempLocals = Object.FindObjectsOfType<LocalBehaviour>(true);
            foreach (var _local in _tempLocals) { locals.Add(_local.GetType(), _local); }
            foreach (var _local in _tempLocals) { _local.OnAwake(); }
            foreach (var _local in _tempLocals) { _local.OnStart(); }
            
            var _tempMultilocals = Object.FindObjectsOfType<MultilocalBehaviour>(true);
            foreach (var _multilocal in _tempMultilocals) { _multilocal.OnAwake(); }
            foreach (var _multilocal in _tempMultilocals) { _multilocal.OnStart(); }
        }

        private static void UnloadLocal()
        {
            locals.Clear();
        }

        public static bool TryGet<T>(out T behaviour) where T : class, IBehaviour
        {
            if (globals == null) { behaviour = null; return false; }
            if (globals.TryGetValue(typeof(T), out var _global) && _global is T _castedGlobal)
            {
                behaviour = _castedGlobal;
                return true;
            }

            if (locals == null) { behaviour = null; return false; }
            if (locals.TryGetValue(typeof(T), out var _local) && _local is T _castedLocal)
            {
                behaviour = _castedLocal;
                return true;
            }

            behaviour = null;
            return false;
        }
        
        public static T Get<T>() where T : class, IBehaviour
        {
            if (globals == null || locals == null) return null;

            if (typeof(GlobalBehaviour).IsAssignableFrom(typeof(T)))
            {
                if (globals.TryGetValue(typeof(T), out var _value)) return _value as T;
            }
            else if (typeof(LocalBehaviour).IsAssignableFrom(typeof(T)))
            {
                if (locals.TryGetValue(typeof(T), out var _value)) return _value as T;
            }

            return null;
        }
    }
}


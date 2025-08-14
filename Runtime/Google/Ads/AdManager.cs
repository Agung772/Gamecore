#if GOOGLE_MOBILE

using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Gamecore.Google
{
    public class AdManager : GlobalBehaviour
    {
        private bool hasInitialize;
        private Dictionary<Type, AdBase> instances = new();
        public GoogleSetting Setting { get; private set; }
        
        public override IEnumerator InitializeCoroutine()
        {
            Setting = Resources.Load<GoogleSetting>("GoogleSetting");
            if (Setting == null) yield break;
            
            MobileAds.Initialize(_=> 
            {
                hasInitialize = true;
                instances = InstanceUtility.Create<AdBase>();
                foreach (var _google in instances.Values)
                {
                    _google.Initialize();
                }
            });
        }


        public bool IsActive<T>() where T : AdBase
        {
            var _instance = Get<T>();
            return _instance != null && _instance.CanShow();
        }
        public T Get<T>() where T : AdBase
        {
            if (instances.TryGetValue(typeof(T), out var _instance))
            {
                return (T)_instance;
            }

            return null;
        }
        public bool TryGet<T>(out T instance) where T : AdBase
        {
            if (hasInitialize)
            {
                var _instance = Get<T>();
                if (_instance != null && _instance.CanShow())
                {
                    instance = _instance;
                    return true;
                }
            }

            instance = null;
            return false;
        }
    }
}

#endif
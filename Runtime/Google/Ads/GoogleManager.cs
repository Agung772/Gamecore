#if GOOGLE_MOBILE_ADS

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoogleMobileAds.Api;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Gamecore.Google
{
    public class GoogleManager : GlobalBehaviour
    {
        private bool hasInitialize;
        private Dictionary<Type, GoogleBase> googles = new();
        public GoogleSetting Setting { get; private set; }

        public override IEnumerator InitializeCoroutine()
        {
            Setting = Resources.Load<GoogleSetting>("");
            if (Setting == null) yield break;

            RequestAdMob(() => { });
            RequestPlayGames(() => { });
            
            googles = InstanceUtility.Create<GoogleBase>();
            foreach (var _google in googles.Values)
            {
                _google.Initialize();
            }
        }

        private async void RequestAdMob(Action onComplete)
        {
            if (Setting.noAds) return;
            if (!await GameNetwork.IsInternetConnection()) return;
            
            MobileAds.Initialize(_=> 
            {
                hasInitialize = true;
            });
        }

        private async void RequestPlayGames(Action onComplete)
        {
            if (!await GameNetwork.IsInternetConnection()) return;
            
            PlayGamesPlatform.Instance.Authenticate(success =>
            {
                if (success == SignInStatus.Success)
                {
                    Debug.Log("Login with Google Play games successful.");
                }
                else
                {
                    Debug.Log("Login Unsuccessful.");
                }
            });
        }
        
        public bool TryGet<T>(out T googleBase) where T : GoogleBase
        {
            if (hasInitialize)
            {
                if (googles.TryGetValue(typeof(T), out var _google))
                {
                    if (_google != null && _google.IsCanShow())
                    {
                        googleBase = (T)_google;
                        return true;
                    }
                }
            }

            googleBase = null;
            return false;
        }

        public bool IsActive<T>() where T : GoogleBase
        {
            if (TryGet<T>(out _))
            {
                return true;
            }
            return false;
        }
    }
}

#endif
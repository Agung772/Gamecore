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
        private Dictionary<Type, GoogleBase> ads = new();
        public GoogleSetting Setting { get; private set; }

        public override IEnumerator InitializeCoroutine()
        {
            Setting = Resources.Load<GoogleSetting>("");
            if (Setting == null) yield break;

            RequestAdMob(() => { });
            RequestPlayGames(() => { });
            
            ads = InstanceUtility.Create<GoogleBase>();
            foreach (var _ad in ads.Values)
            {
                _ad.Initialize();
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
        
        public bool TryGet<T>(out T ad) where T : GoogleBase
        {
            if (hasInitialize)
            {
                if (ads.TryGetValue(typeof(T), out var _ad))
                {
                    if (_ad != null && _ad.IsCanShow())
                    {
                        ad = (T)_ad;
                        return true;
                    }
                }
            }

            ad = null;
            return false;
        }
    }
}

#endif
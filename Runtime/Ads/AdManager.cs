#if GOOGLE_MOBILE_ADS

using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Gamecore.Ads
{
    public class AdManager : GlobalBehaviour
    {
        private bool hasInitialize;
        private Dictionary<Type, AdBase> ads;
        public AdSetting Setting { get; private set; }

        public override async void Initialize()
        {
            Setting = Resources.Load<AdSetting>("AdSetting");
            if (Setting == null) return;
            
            ads = new Dictionary<Type, AdBase>();

            if (await GameNetwork.IsInternetConnection() && !Setting.noAds)
            {
                MobileAds.Initialize(_=> 
                {
                    hasInitialize = true;
                    ads = InstanceUtility.Create<AdBase>();
                    foreach (var _ad in ads.Values)
                    {
                        _ad.Initialize();
                    }
                });
            }
        }

        public bool TryGet<T>(out T ad) where T : AdBase
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
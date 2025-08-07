#if GOOGLE_MOBILE_ADS

using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Gamecore.Ads
{
    public class AdRewarded : AdBase
    {
        private RewardedAd rewardedAd;

        public override void Initialize()
        {
            Request();
        }
        
        public override bool IsCanShow()
        {
            return rewardedAd != null && rewardedAd.CanShowAd();
        }

        private void Request()
        {
            var _request = new AdRequest();

            RewardedAd.Load(Game.Get<AdManager>().Setting.rewardedID, _request, (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("RewardedAd failed to load");
                    return;
                }
                
                rewardedAd = ad;
                rewardedAd.OnAdFullScreenContentClosed += Request;
            });
        }
        
        public void Show(Action onComplete, Action onFailed)
        {
            if (IsCanShow())
            {
                var _isRewarded = false;

                rewardedAd.Show((Reward reward) =>
                {
                    _isRewarded = true;
                });

                rewardedAd.OnAdFullScreenContentClosed += () =>
                {
                    if (_isRewarded)
                    {
                        onComplete?.Invoke();
                    }
                    else
                    {
                        onFailed?.Invoke();
                    }
                };
            }
            else
            {
                onFailed?.Invoke();
                Request();
            }
        }
    }
}

#endif
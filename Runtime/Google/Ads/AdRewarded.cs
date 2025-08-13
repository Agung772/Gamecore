#if GOOGLE_MOBILE_ADS

using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Gamecore.Google
{
    public class AdRewarded : GoogleBase
    {
        private RewardedAd data;

        public override void Initialize()
        {
            Request();
        }
        
        public override bool IsCanShow()
        {
            return data != null && data.CanShowAd();
        }

        private void Request()
        {
            var _request = new AdRequest();

            RewardedAd.Load(Game.Get<GoogleManager>().Setting.rewardedID, _request, (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("RewardedAd failed to load");
                    return;
                }
                
                data = ad;
                data.OnAdFullScreenContentClosed += Request;
            });
        }
        
        public void Show(Action onComplete, Action onFailed)
        {
            if (IsCanShow())
            {
                var _isRewarded = false;

                data.Show((Reward reward) =>
                {
                    _isRewarded = true;
                });

                data.OnAdFullScreenContentClosed += () =>
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
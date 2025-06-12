using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Core.Ads
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
        

        public void Show(Action onRewarded, Action onFailed, Action onClosed)
        {
            if (IsCanShow())
            {
                rewardedAd.Show((Reward reward) =>
                {
                    onRewarded?.Invoke();
                });
                rewardedAd.OnAdFullScreenContentClosed += () =>
                {
                    onClosed?.Invoke();
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



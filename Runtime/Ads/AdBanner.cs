#if GOOGLE_MOBILE_ADS

using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Gamecore.Ads
{
    public class AdBanner : AdBase
    {
        private BannerView bannerView;
        public bool IsShow { get; private set; }

        public override void Initialize()
        {
            Request(checkConnection: false);
            Game.Manager.StartCoroutine(Refresh());
            Game.Get<SceneLoader>().OnLoad += Show;
            Game.Get<SceneLoader>().OnUnlooad += Hide;
        }

        public override bool IsCanShow()
        {
            return bannerView != null;
        }

        private IEnumerator Refresh()
        {
            while (true)
            {
                yield return new WaitForSeconds(30f);
                Request();
            }
        }
        
        public void Show()
        {
            if (IsShow) return;
            
            bannerView.Show();
            IsShow = true;
        }
        public void Hide()
        {
            if (!IsShow) return;
            
            bannerView.Hide();
            IsShow = false;
        }
        private async void Request(bool checkConnection = true)
        {
            if (bannerView != null)
            {
                Hide();
                bannerView.Destroy();
            }
            
            if (!checkConnection || await GameNetwork.IsInternetConnection())
            {
                bannerView = new BannerView(Game.Get<AdManager>().Setting.bannerID, AdSize.Banner, AdPosition.Top);
                bannerView.OnBannerAdLoaded += BannerAdLoaded;
            
                var _request = new AdRequest();
                IsShow = true;
                bannerView.LoadAd(_request);
            }
        }
        
        private void BannerAdLoaded()
        {
            Debug.Log("Banner Ad Loaded");
        }
    }
}

#endif
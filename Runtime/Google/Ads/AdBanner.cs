#if GOOGLE_MOBILE_ADS

using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Gamecore.Google
{
    public class AdBanner : GoogleBase
    {
        private BannerView data;
        public bool IsShow { get; private set; }

        public override void Initialize()
        {
            Request(checkConnection: false);
            Game.Manager.StartCoroutine(Refresh());
            Game.Get<SceneLoader>().OnLoaded += Show;
            Game.Get<SceneLoader>().OnUnloaded += Hide;
        }

        public override bool IsCanShow()
        {
            return data != null;
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
            
            data.Show();
            IsShow = true;
        }
        public void Hide()
        {
            if (!IsShow) return;
            
            data.Hide();
            IsShow = false;
        }
        private async void Request(bool checkConnection = true)
        {
            if (data != null)
            {
                Hide();
                data.Destroy();
            }
            
            if (!checkConnection || await GameNetwork.IsInternetConnection())
            {
                data = new BannerView(Game.Get<AdManager>().Setting.bannerID, AdSize.Banner, AdPosition.Top);
                data.OnBannerAdLoaded += BannerAdLoaded;
            
                var _request = new AdRequest();
                IsShow = true;
                data.LoadAd(_request);
            }
        }
        
        private void BannerAdLoaded()
        {
            Debug.Log("Banner Ad Loaded");
        }
    }
}

#endif
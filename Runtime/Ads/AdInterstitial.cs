#if GOOGLE_MOBILE_ADS

using GoogleMobileAds.Api;
using UnityEngine;

namespace Gamecore.Ads
{
    public class AdInterstitial : AdBase
    {
        private InterstitialAd interstitialAd;

        public override void Initialize()
        {
            Request();
        }
        private void Request()
        {
            var _adRequest = new AdRequest();

            InterstitialAd.Load(Game.Get<AdManager>().Setting.interstitialID, _adRequest,
                (ad, error) =>
                {
                    if (error != null || ad == null)
                    {
                        Debug.LogError("Gagal load Interstitial: " + error);
                        return;
                    }

                    Debug.Log("Interstitial berhasil dimuat");
                    interstitialAd = ad;

                    interstitialAd.OnAdFullScreenContentClosed += () =>
                    {
                        Debug.Log("Interstitial ditutup, load ulang lagi");
                        Request();
                    };
                });
        }

        public void Show()
        {
            if (interstitialAd != null && interstitialAd.CanShowAd())
            {
                interstitialAd.Show();
            }
            else
            {
                Debug.Log("Interstitial belum siap, loading dulu...");
                Request();
            }
        }
    }
}

#endif
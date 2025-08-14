#if GOOGLE_MOBILE

using GoogleMobileAds.Api;
using UnityEngine;

namespace Gamecore.Google
{
    public class AdInterstitial : AdBase
    {
        private InterstitialAd data;

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
                    data = ad;

                    data.OnAdFullScreenContentClosed += () =>
                    {
                        Debug.Log("Interstitial ditutup, load ulang lagi");
                        Request();
                    };
                });
        }

        public void Show()
        {
            if (data != null && data.CanShowAd())
            {
                data.Show();
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
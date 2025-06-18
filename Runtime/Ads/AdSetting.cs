using UnityEngine;

namespace Core.Ads
{
    [CreateAssetMenu(menuName = "Core/Ad Setting")]
    public class AdSetting : ScriptableObject
    {
        public bool noAds;
    
        public string bannerID = "ca-app-pub-3940256099942544/6300978111"; // Test ID
        public string interstitialID = "ca-app-pub-3940256099942544/1033173712"; // Test ID
        public string rewardedID = "ca-app-pub-3940256099942544/5224354917"; // Test ID
    }
}


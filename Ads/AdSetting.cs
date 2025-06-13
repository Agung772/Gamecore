using UnityEngine;

namespace Core.Ads
{
    [CreateAssetMenu(menuName = "Core/Ad Setting")]
    public class AdSetting : ScriptableObject
    {
        public bool noAds;
    
        public string bannerID;
        public string interstitialID;
        public string rewardedID;
    }
}


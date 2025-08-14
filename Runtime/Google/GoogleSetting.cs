#if GOOGLE_MOBILE

using Sirenix.OdinInspector;
using UnityEngine;

namespace Gamecore.Google
{
    [CreateAssetMenu(menuName = "Gamecore/Ad Setting")]
    public class GoogleSetting : ScriptableObject
    {
        [FoldoutGroup("Ad")] public bool noAds;
        [FoldoutGroup("Ad")] public string bannerID = "ca-app-pub-3940256099942544/6300978111"; // Test ID
        [FoldoutGroup("Ad")] public string interstitialID = "ca-app-pub-3940256099942544/1033173712"; // Test ID
        [FoldoutGroup("Ad")] public string rewardedID = "ca-app-pub-3940256099942544/5224354917"; // Test ID

        [FoldoutGroup("Play Games")] public string leaderboardID;
    }
}

#endif
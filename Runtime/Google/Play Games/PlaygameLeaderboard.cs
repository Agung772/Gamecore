#if GOOGLE_MOBILE

using GooglePlayGames;
using UnityEngine;

namespace Gamecore.Google
{
    public class PlaygameLeaderboard : AdBase
    {
        public override bool CanShow()
        {
            return true;
        }

        public void ReportScore(long skor)
        {
            PlayGamesPlatform.Instance.ReportScore(skor, Game.Get<AdManager>().Setting.leaderboardID, success =>
            {
                Debug.Log(success ? "Skor terkirim" : "Gagal kirim skor");
            });
        }
        
        public void Show()
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
    }
}

#endif
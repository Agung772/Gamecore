using GooglePlayGames;
using UnityEngine;

namespace Gamecore.Google
{
    public class GoogleLeaderboard : GoogleBase
    {
        public override bool IsCanShow()
        {
            return true;
        }

        public void ReportScore(long skor)
        {
            PlayGamesPlatform.Instance.ReportScore(skor, Game.Get<GoogleManager>().Setting.leaderboardID, success =>
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

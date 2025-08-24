#if GOOGLE_MOBILE

using GooglePlayGames;
using UnityEngine;

namespace Gamecore.Google
{
    public class PlayGamesLeaderboard : PlayGamesBase
    {
        public void ReportScore(long skor)
        {
            Social.ReportScore(skor, Game.Get<PlayGamesManager>().Setting.leaderboardID, success =>
            {
                Debug.Log(success ? "Skor terkirim" : "Gagal kirim skor");
            });
        }
        
        public void Show()
        {
            Social.ShowLeaderboardUI();
        }
    }
}

#endif
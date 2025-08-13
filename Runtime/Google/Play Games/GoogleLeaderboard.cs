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
            PlayGamesPlatform.Instance.ReportScore(skor, "CgkI6NbynIAVEAIQAA", sukses =>
            {
                Debug.Log(sukses ? "Skor terkirim" : "Gagal kirim skor");
            });
        }
        
        public void Show()
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
    }
}

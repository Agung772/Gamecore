using UnityEngine;

namespace Gamecore
{
    public class GoogleLeaderboard
    {
        public void ReportScore(long skor)
        {
            Social.ReportScore(skor, "CgkI6NbynIAVEAIQAA", sukses =>
            {
                Debug.Log(sukses ? "Skor terkirim" : "Gagal kirim skor");
            });
        }
        
        public void Show()
        {
            Social.ShowLeaderboardUI();
        }
    }
}

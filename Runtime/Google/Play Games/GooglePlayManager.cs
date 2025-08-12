using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

namespace Gamecore
{
    public class GooglePlayManager : GlobalBehaviour
    {
        public bool IsAuthenticate {  get; private set; }
        public GoogleLeaderboard Leaderboard {  get; private set; }
        public override void Initialize()
        {
            base.Initialize();
            PlayGamesPlatform.Activate();
            Leaderboard = new GoogleLeaderboard();
            PlayGamesPlatform.Instance.Authenticate((success) =>
            {
                if (success == SignInStatus.Success)
                {
                    IsAuthenticate = true;
                    Debug.Log("Login with Google Play games successful.");
                }
                else
                {
                    Debug.Log("Login Unsuccessful");
                }
            });
        }
    }
}


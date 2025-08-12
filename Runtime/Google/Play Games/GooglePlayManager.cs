using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

namespace Gamecore
{
    public class GooglePlayManager : GlobalBehaviour
    {
        public GoogleLeaderboard Leaderboard {  get; private set; }
        public override void Initialize()
        {
            base.Initialize();
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.Authenticate((success) =>
            {
                if (success == SignInStatus.Success)
                {
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


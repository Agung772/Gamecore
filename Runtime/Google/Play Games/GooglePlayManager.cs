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
            Debug.Log("Loading login with Google Play games...");
            Leaderboard = new GoogleLeaderboard();
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate(success =>
            {
                if (success)
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


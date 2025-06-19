using UnityEngine;

namespace Gamecore
{
    public class SceneStarted : LocalBehaviour
    {
        [SerializeField] private AudioClip sceneBGM;
        public override void OnStart()
        {
            sceneBGM.PlayLoop();
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Core
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


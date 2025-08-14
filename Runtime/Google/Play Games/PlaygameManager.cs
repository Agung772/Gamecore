#if GOOGLE_MOBILE

using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;


namespace Gamecore.Google
{
    public class PlaygameManager : GlobalBehaviour
    { 
        private bool hasInitialize;
        private int maxTryAgain = 3;
        private int countTryAgain;
        public event Action OnInitialize;
        private Dictionary<Type, PlaygameBase> instances = new();
        public GoogleSetting Setting { get; private set; }

        public override IEnumerator InitializeCoroutine()
        {
            Setting = Resources.Load<GoogleSetting>("GoogleSetting");
            if (Setting == null) yield break;

            RequestPlayGames(() =>
            {
                hasInitialize = true;
                instances = InstanceUtility.Create<PlaygameBase>();
                foreach (var _google in instances.Values)
                {
                    _google.Initialize();
                }
                OnInitialize?.Invoke();
            });
        }

        private void RequestPlayGames(Action onComplete)
        {
            PlayGamesPlatform.Instance.Authenticate(success =>
            {
                if (success == SignInStatus.Success)
                {
                    onComplete.Invoke();
                    Debug.Log("Login with Google Play games successful.");
                }
                else
                {
                    if (countTryAgain < maxTryAgain)
                    {
                        countTryAgain++;
                        Game.Manager.gameObject.LeanDelayedCall(1f, () =>
                        {
                            RequestPlayGames(onComplete);
                        });
                    }
 
                    Debug.Log("Login Unsuccessful.");
                }
            });
        }
        
        public bool IsActive<T>() where T : PlaygameBase
        {
            return hasInitialize;
        }
        public T Get<T>() where T : PlaygameBase
        {
            if (instances.TryGetValue(typeof(T), out var _instance))
            {
                return (T)_instance;
            }

            return null;
        }
        public bool TryGet<T>(out T instance) where T : PlaygameBase
        {
            if (hasInitialize)
            {
                var _instance = Get<T>();
                if (_instance != null)
                {
                    instance = _instance;
                    return true;
                }
            }

            instance = null;
            return false;
        }
    }
}

#endif
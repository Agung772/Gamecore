using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gamecore
{
    public class SceneLoader : GlobalBehaviour
    {
        public event Action OnLoad;
        public event Action OnUnlooad;

        public override void Initialize()
        {
            Game.CurrentScene = SceneManager.GetActiveScene().name;
            SceneManager.sceneLoaded += (_, _) => OnLoad?.Invoke();
            SceneManager.sceneUnloaded += _ => OnUnlooad?.Invoke();
        }

        public void RestratScene()
        {
            LoadScene(Game.CurrentScene);
        }
        
        public void LoadScene(string sceneName, Action<float> onProgress = null, Action onComplete = null)
        {
            Game.Manager.StartCoroutine(LoadSceneAsync(sceneName, onProgress, onComplete));
        }

        private IEnumerator LoadSceneAsync(string sceneName, Action<float> onProgress = null, Action onComplete = null)
        {
            var _async = SceneManager.LoadSceneAsync(sceneName);

            while (_async.isDone)
            {
                var _progress = Mathf.Clamp01(_async.progress / 0.9f);
                onProgress?.Invoke(_progress);
                yield return null;
            }

            var _isCompleted = false;
            _async.completed += _ => _isCompleted = true;
            yield return new WaitUntil(() => _isCompleted);
            
            Game.CurrentScene = SceneManager.GetActiveScene().name;
            onComplete?.Invoke();
        }
    }
}


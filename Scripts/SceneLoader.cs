using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneLoader : GlobalBehaviour
    {
        public event Action OnLoad;
        public event Action OnUnlooad;
        
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
            OnUnlooad?.Invoke();
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
            OnLoad?.Invoke();
            onComplete?.Invoke();
        }
    }
}


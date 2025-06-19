using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gamecore
{
    [DefaultExecutionOrder(-1000)]
    public class GameManager : MonoBehaviour
    {
        public RectTransform Canvas => canvas;
        [SerializeField] private RectTransform canvas;
        
        #if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnPlay()
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                Instantiate(Resources.Load<GameManager>("GameManager"));
            }
        }
        #endif

        private void Awake()
        {
            StartCoroutine(Initialize());
        }

        private IEnumerator Initialize()
        {
            DontDestroyOnLoad(gameObject);
            Game.Manager = this;
            Game.Initialize();
            yield return Game.InitializeCoroutine();
            
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                var _scenePath = SceneUtility.GetScenePathByBuildIndex(1);
                var _sceneName = Path.GetFileNameWithoutExtension(_scenePath);
                Game.Get<SceneLoader>().LoadScene(_sceneName);
            }
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                if (Game.TryGet<Storage>(out var _storage))
                {
                    _storage.Save();
                }
            }
        }
    }
}


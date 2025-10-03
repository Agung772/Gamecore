using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ACore
{
    [DefaultExecutionOrder(-1000)]
    public class GameManager : MonoBehaviour
    {
        public RectTransform Canvas => canvas; [SerializeField] private RectTransform canvas;
        public RectTransform CanvasPrefab => canvasPrefab; [SerializeField] private RectTransform canvasPrefab;
        
        #if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnPlay()
        {
            Debug.Log($"{nameof(ACore)}: Play");
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                Debug.Log($"{nameof(ACore)}: Create Manager");
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
            Debug.Log($"{nameof(ACore)}: Initialize");
            yield return Game.InitializeCoroutine();
            Debug.Log($"{nameof(ACore)}: Initialize Coroutine");
            
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


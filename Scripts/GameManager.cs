using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
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
                
                var _prefab = Resources.Load<GameManager>("GameManager");
                Instantiate(_prefab);
            }
        }
        #endif
        private void Awake()
        {
            Game.Manager = this;
            GameLoader.Initialize();
            Game.Initialize();
            DontDestroyOnLoad(gameObject);
            if (Game.CurrentScene == "Launcher")
            {
                GameLoader.LoadScene(SceneManager.GetSceneByBuildIndex(1).name);
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


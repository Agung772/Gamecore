using System.Collections;
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
                Game.CurrentScene = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(0);
            }
        }
        #endif
        private IEnumerator Start()
        {
            DontDestroyOnLoad(gameObject);
            Game.Manager = this;
            yield return Game.Initialize();
            Game.Get<SceneLoader>().LoadScene(Game.CurrentScene);
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


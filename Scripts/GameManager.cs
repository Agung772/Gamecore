using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
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
        private void Start()
        {
            Game.Manager = this;
            GameLoader.Initialize();
            Game.Initialize();
            DontDestroyOnLoad(gameObject);
            if (Game.CurrentScene == "Launcher")
            {
                GameLoader.LoadScene("EatDrink");
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}


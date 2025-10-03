using System.Collections.Generic;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Overlays;
using UnityEditor.SceneManagement;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ACore.Tool
{
    [Overlay(typeof(SceneView), "Scene", true)]
    public class SceneToolbarOverlay : ToolbarOverlay{
        private SceneToolbarOverlay() : base(GameScene.ID, OpenMode.ID) { }
        private static OpenSceneMode openSceneMode;

        [EditorToolbarElement(ID, typeof(SceneView))]
        private class OpenMode : EditorToolbarToggle{
            private bool isActive;

            public OpenMode() {
                text = isActive ? " Additive" : " Single";
                openSceneMode = isActive ? OpenSceneMode.Additive : OpenSceneMode.Single;
            }

            public override bool value {
                get => isActive;
                set {
                    isActive = value;
                    openSceneMode = isActive ? OpenSceneMode.Additive : OpenSceneMode.Single;
                    text = value ? " Additive" : " Single";
                }
            }

            public const string ID = "SceneToolbarOverlay/OpenMode";
        }

        [EditorToolbarElement(ID, typeof(SceneView))]
        private class GameScene : EditorToolbarButton{
            public GameScene() {
                text = "Scene";
                clicked += LoadScene;
                sceneSearchProvider = ScriptableObject.CreateInstance<SceneSearchProvider>();
            }

            public const string ID = "SceneToolbarOverlay/GameScene";
            private readonly SceneSearchProvider sceneSearchProvider;

            private void LoadScene() {
                SearchWindow.Open(new SearchWindowContext(Event.current.mousePosition + Vector2.up * 100), sceneSearchProvider);
            }
        }

        public class SceneSearchProvider : ScriptableObject, ISearchWindowProvider{
            public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context) {
                var _tree = new List<SearchTreeEntry> {
                    new SearchTreeGroupEntry(new GUIContent("Scenes")),
                };
                var _scenes = GetScenes();
                foreach (var _scene in _scenes) {
                    var _pathScene = AssetDatabase.GetAssetPath(_scene);
                    _tree.Add(new SearchTreeEntry(new GUIContent(_scene.name, EditorIcons.UnityLogo)) {
                        level = 1, userData = _pathScene
                    });
                }

                return _tree;
            }

            public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context) {
                if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return true;
                SceneManager.SetActiveScene(EditorSceneManager.OpenScene((string)entry.userData, openSceneMode));
                return true;
            }

            private List<SceneAsset> GetScenes() {
                var _sceneAssets = new List<SceneAsset>();

                foreach (var _scene in EditorBuildSettings.scenes)
                {
                    if (!_scene.enabled) continue;

                    var _asset = AssetDatabase.LoadAssetAtPath<SceneAsset>(_scene.path);
                    if (_asset != null)
                    {
                        _sceneAssets.Add(_asset);
                    }
                }

                return _sceneAssets;
            }
        }
    }
}
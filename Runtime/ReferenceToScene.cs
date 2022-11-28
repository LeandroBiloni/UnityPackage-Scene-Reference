using UnityEngine;

namespace SceneReference
{
    [CreateAssetMenu(fileName = "SceneReference", menuName = "Scriptable Objects/Scene Reference")]
    public class ReferenceToScene : ScriptableObject
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private string _sceneGuid;
        public string SceneName => _sceneName;
        public string SceneGUID => _sceneGuid;
        public void SetSceneName(string name) => _sceneName = name;
        public void SetGUID(string guid) => _sceneGuid = guid;
    }
}


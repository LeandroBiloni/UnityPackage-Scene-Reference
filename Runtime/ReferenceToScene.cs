using UnityEngine;

namespace SceneReference
{
    [CreateAssetMenu(fileName = "SceneReference", menuName = "Scriptable Objects/Scene Reference")]
    public class ReferenceToScene : ScriptableObject
    {
        public string sceneName;
        public string sceneGuid;
    }
}


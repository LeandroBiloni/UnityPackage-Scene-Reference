using UnityEngine;

namespace SceneReference
{
    public class ReferenceToScene : ScriptableObject
    {
        public string sceneName;
        [HideInInspector] public string sceneGuid;

        public void Configure(string name, string guid)
        {
            sceneName = name;
            
            sceneGuid = guid;
        }
    }
}


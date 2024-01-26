#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace SceneReference
{
    [CustomEditor(typeof(SceneAsset))]
    public class SceneAssetEditor : Editor
    {
        public override void OnInspectorGUI ()
        {
            GUI.enabled = true;

            if (GUILayout.Button("Create Scene Reference"))
            {
                Debug.Log("create");
                ReferenceToSceneCreator.Create();
            }
        }
    }
}
#endif
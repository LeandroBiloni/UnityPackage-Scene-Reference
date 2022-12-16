#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using SceneReference.Utils;
using System.Collections.Generic;

namespace SceneReference
{
    [CustomEditor(typeof(ReferenceToScene))]
    public class ReferenceToSceneEditor : Editor
    {
        private SceneAsset _sceneAsset;
        private ReferenceToScene _target;
        private void OnEnable()
        {
            _target = (ReferenceToScene)target;

        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.LabelField("Scene GUID ", _target.sceneGuid);

            _sceneAsset = EditorGUILayout.ObjectField("Config from Scene", _sceneAsset, typeof(SceneAsset), false) as SceneAsset;
                        
            if (_sceneAsset != null)
                SetSceneDataToReference(_sceneAsset);

            if (GUILayout.Button("Get Scene Name by GUID"))
                GetSceneNameByGUID();
        }

        private void SetSceneDataToReference(SceneAsset scene)
        {
            string sceneAssetPath = AssetDatabase.GetAssetPath(scene);

            GUID guid = AssetDatabase.GUIDFromAssetPath(sceneAssetPath);

            if (GUIDChecker.ExistsAnotherSceneReferenceWithSameGUID(guid.ToString()))
            {
                Debug.LogError("Another Reference to Scene " + scene.name + " (GUID: " + guid.ToString() + ") already exists!");

                _sceneAsset = null;
                return;
            }

            _target.Configure(scene.name, guid.ToString());                

            EditorUtility.SetDirty(_target);

            AssetDatabase.SaveAssetIfDirty(_target);

            _sceneAsset = null;

            AssetRenamer.RenameAsset(_target, scene.name + " Reference");
        }

        private void GetSceneNameByGUID()
        {
            string guid = _target.sceneGuid;
            if (string.IsNullOrEmpty(guid))
            {
                Debug.LogError("There is no guid stored!");
                return;
            }

            string scenePath = AssetDatabase.GUIDToAssetPath(guid);

            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);

            if (sceneAsset == null)
            {
                Debug.LogError($"There is no asset with {guid} GUID!");
                return;
            }
            _sceneAsset = sceneAsset;

            SetSceneDataToReference(_sceneAsset);
        }
    }
}
#endif
#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using SceneReference.Utils;

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

            if (!string.IsNullOrEmpty(_target.sceneGuid))
            {
                _sceneAsset = GetSceneAsset(_target.sceneGuid);
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.LabelField("Scene GUID ", _target.sceneGuid);

            _sceneAsset = EditorGUILayout.ObjectField("Scene", _sceneAsset, typeof(SceneAsset), false) as SceneAsset;
                        
            if (_sceneAsset != null && _sceneAsset.name != _target.sceneName)
                SetSceneDataToReference(_sceneAsset);

            if (GUILayout.Button(new GUIContent("Get Scene Name by GUID", "Use when the scene name in the scriptable object was manually changed.")))
                GetSceneNameByGUID();
        }

        private void SetSceneDataToReference(SceneAsset scene)
        {
            string sceneAssetPath = AssetDatabase.GetAssetPath(scene);

            GUID guid = AssetDatabase.GUIDFromAssetPath(sceneAssetPath);

            if (GUIDChecker.ExistsAnotherSceneReferenceWithSameGUID(guid.ToString()))
            {
                Debug.LogError($"Another Reference to Scene {scene.name} (GUID: guid.ToString() already exists!");

                _sceneAsset = GetSceneAsset(_target.sceneGuid);

                return;
            }

            _target.Configure(scene.name, guid.ToString());                

            EditorUtility.SetDirty(_target);

            AssetDatabase.SaveAssetIfDirty(_target);

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

            SceneAsset sceneAsset = GetSceneAsset(guid);

            if (sceneAsset == null)
            {
                Debug.LogError($"There is no asset with {guid} GUID!");
                return;
            }

            _sceneAsset = sceneAsset;

            SetSceneDataToReference(_sceneAsset);
        }

        private SceneAsset GetSceneAsset(string sceneGUID)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(sceneGUID);

            return AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
        }
    }
}
#endif
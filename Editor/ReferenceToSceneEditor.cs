using UnityEngine;
using UnityEditor;
using Utilities.AssetDatabaseUtils;

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

            _sceneAsset = EditorGUILayout.ObjectField("Scene", _sceneAsset, typeof(SceneAsset), false) as SceneAsset;

            if (_sceneAsset != null)
                SetSceneDataToReference(_sceneAsset);

            if (GUILayout.Button("Get Scene Name by GUID"))
                GetSceneNameByGUID();
        }

        private void SetSceneDataToReference(SceneAsset scene)
        {
            string sceneAssetPath = AssetDatabase.GetAssetPath(scene);

            GUID guid = AssetDatabase.GUIDFromAssetPath(sceneAssetPath);

            _target.SetSceneName(scene.name);
            _target.SetGUID(guid.ToString());

            AssetRenamer.RenameAsset(_target, scene.name + " Reference");
        }

        private void GetSceneNameByGUID()
        {
            string guid = _target.SceneGUID;
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
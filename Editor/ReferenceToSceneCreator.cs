#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using SceneReference.Utils;

namespace SceneReference
{
    public class ReferenceToSceneCreator
    {
        [MenuItem("Assets/Create/Scriptable Objects/Scene Reference")]
        public static void Create()
        {
            ReferenceToScene referenceToScene = ScriptableObject.CreateInstance<ReferenceToScene>();

            string sceneAssetPath = AssetDatabase.GetAssetPath(Selection.activeObject);

            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath(sceneAssetPath, typeof(SceneAsset)) as SceneAsset;

            string sceneName = sceneAsset.name;
            string sceneGUID = AssetDatabase.GUIDFromAssetPath(sceneAssetPath).ToString();

            referenceToScene.Configure(sceneName, sceneGUID);

            string sceneNameWithExtension = sceneAsset.name + ".unity";

            int indexToStartRemovingFileName = sceneAssetPath.Length - sceneNameWithExtension.Length;

            string folderPath = sceneAssetPath.Remove(indexToStartRemovingFileName, sceneNameWithExtension.Length);

            string pathWithExtension = folderPath + sceneName + " Reference.asset";

            string finalPath = AssetDatabase.GenerateUniqueAssetPath(pathWithExtension);

            AssetDatabase.CreateAsset(referenceToScene, finalPath);

            ReferenceToScene loadedSceneReference = AssetDatabase.LoadAssetAtPath(finalPath, typeof(ReferenceToScene)) as ReferenceToScene;

            EditorUtility.SetDirty(loadedSceneReference);

            AssetDatabase.SaveAssetIfDirty(loadedSceneReference);

            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Create/Scriptable Objects/Scene Reference", true)]
        public static bool CreateValidation()
        {
            if (Selection.activeObject.GetType() != typeof(SceneAsset))
            {
                Debug.LogWarning("Selected asset is not a scene!");
                return false;
            }

            string sceneAssetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            string guid = AssetDatabase.GUIDFromAssetPath(sceneAssetPath).ToString();

            if (!CanCreateWithThisGUID(guid))
            {
                Debug.LogWarning("A Reference to this Scene already exists!");
                return false;
            }
                
            return true;
        }

        private static bool CanCreateWithThisGUID(string guid)
        {
            return !GUIDChecker.ExistsAnotherSceneReferenceWithSameGUID(guid);
        }
    }
}
#endif
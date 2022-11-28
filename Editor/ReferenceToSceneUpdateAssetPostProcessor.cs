using UnityEditor;
using UnityEngine;
using Utilities.AssetDatabaseUtils;

namespace SceneReference
{
    public class ReferenceToSceneUpdateAssetPostProcessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string assetPath in movedAssets)
            {
                Object asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(SceneAsset));

                if (asset == null)
                    continue;

                SceneAsset scene = (SceneAsset)asset;

                if (scene == null)
                    return;

                GUID sceneGuid = AssetDatabase.GUIDFromAssetPath(assetPath);

                string[] assetsPaths = AssetDatabase.FindAssets("t:ReferenceToScene");

                foreach (string guid in assetsPaths)
                {
                    ReferenceToScene sceneReference = GetReferenceToSceneFromGuid(guid);

                    if (sceneReference == null) 
                        continue;

                    if (sceneReference.SceneGUID != sceneGuid.ToString())
                        continue;
                    
                    AssetRenamer.RenameAsset(sceneReference, scene.name + " Reference");

                    sceneReference = GetReferenceToSceneFromGuid(guid);

                    sceneReference.SetSceneName(scene.name);
                    sceneReference.SetGUID(sceneGuid.ToString());

                    EditorUtility.SetDirty(sceneReference);
                    
                    AssetDatabase.SaveAssets();
                    break;
                }
            }
        }

        private static ReferenceToScene GetReferenceToSceneFromGuid(string guid)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);

            Object sceneReferenceObj = AssetDatabase.LoadAssetAtPath(path, typeof(ReferenceToScene));

            if (sceneReferenceObj == null)
                return null;

            return (ReferenceToScene)sceneReferenceObj;
        }
    }
}



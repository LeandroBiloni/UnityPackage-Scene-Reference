#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using SceneReference.Utils;

namespace SceneReference
{
    public class ReferenceToSceneUpdateAssetPostProcessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            List<ReferenceToScene> references = new List<ReferenceToScene>();

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

                    if (sceneReference.sceneGuid != sceneGuid.ToString())
                        continue;
                    
                    AssetRenamer.RenameAsset(sceneReference, scene.name + " Reference");

                    sceneReference = GetReferenceToSceneFromGuid(guid);

                    sceneReference.Configure(scene.name, sceneGuid.ToString());

                    EditorUtility.SetDirty(sceneReference);

                    references.Add(sceneReference);
                    break;
                }
            }

            foreach (var r in references)
            {
                AssetDatabase.SaveAssetIfDirty(r);
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
#endif
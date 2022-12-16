#if UNITY_EDITOR
using UnityEditor;
using System.Collections.Generic;

namespace SceneReference.Utils
{
    public static class GUIDChecker
    {
        public static bool ExistsAnotherSceneReferenceWithSameGUID(string guid)
        {
            string[] allReferencesToSceneGUIDS = AssetDatabase.FindAssets("t:ReferenceToScene");

            List<string> allReferenceToScenesPaths = new List<string>();

            foreach (string g in allReferencesToSceneGUIDS)
            {
                allReferenceToScenesPaths.Add(AssetDatabase.GUIDToAssetPath(g));
            }


            foreach (string path in allReferenceToScenesPaths)
            {
                ReferenceToScene referenceToScene = AssetDatabase.LoadAssetAtPath(path, typeof(ReferenceToScene)) as ReferenceToScene;

                if (referenceToScene.sceneGuid == guid)
                    return true;
            }

            return false;
        }
    }
}

#endif
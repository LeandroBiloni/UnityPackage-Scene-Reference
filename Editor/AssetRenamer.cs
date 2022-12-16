#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace SceneReference.Utils
{
    public static class AssetRenamer
    {
        public static void RenameAsset(Object obj, string newName)
        {
            string path = AssetDatabase.GetAssetPath(obj);

            string result = AssetDatabase.RenameAsset(path, newName);

            if (!string.IsNullOrEmpty(result))
                Debug.Log(result);
        }
    }
}

#endif
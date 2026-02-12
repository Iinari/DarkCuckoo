using UnityEditor;
using UnityEngine;
using UnityEditor.Scripting;


public static class ClearMissingTypesUtility
{
    [MenuItem("Tools/Clear Missing SerializeReference")]
    static void ClearSerializeReference()
    {
        foreach (var guid in AssetDatabase.FindAssets("t:ScriptableObject"))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var obj = AssetDatabase.LoadAssetAtPath<Object>(path);
            if (obj != null)
                SerializationUtility.ClearAllManagedReferencesWithMissingTypes(obj);
        }

        AssetDatabase.SaveAssets();
        Debug.Log("Cleared missing SerializeReference types in ScriptableObjects.");
    }
}
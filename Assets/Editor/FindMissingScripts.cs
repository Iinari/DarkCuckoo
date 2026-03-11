using UnityEngine;
using UnityEditor;

public class FindMissingScripts
{
    [MenuItem("Tools/Find Missing Scripts")]
    static void Find()
    {
        GameObject[] gos = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject go in gos)
        {
            Component[] components = go.GetComponents<Component>();

            foreach (Component c in components)
            {
                if (c == null)
                {
                    Debug.Log("Missing script on: " + go.name, go);
                }
            }
        }
    }
}
using UnityEngine;

public class MapSceneController : MonoBehaviour
{
    public MapGenerator mapGenerator; // assign in inspector or leave null to auto-find
    public MapUI mapUI;               // assign in inspector or leave null to auto-find

    void Start()
    {
        if (mapGenerator == null) mapGenerator = GetComponent<MapGenerator>() ?? FindFirstObjectByType<MapGenerator>();
        if (mapUI == null) mapUI = GetComponent<MapUI>() ?? FindFirstObjectByType<MapUI>();

        if (mapGenerator == null || mapUI == null)
        {
            Debug.LogError("MapSceneController: Missing MapGenerator or MapUI. Assign them in the inspector.");
            return;
        }

        var nodes = mapGenerator.GenerateMap();
        mapUI.BuildFromNodes(nodes);
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MapUI : MonoBehaviour
{
    [Header("References")]
    // `mapContainer` must be the ScrollRect.Content RectTransform (the Content of a UI ScrollView)
    public RectTransform mapContainer;
    public RectTransform viewport;    // the ScrollView viewport RectTransform
    public Button nodeButtonPrefab;
    public Image lineImagePrefab;

    [Header("Visuals")]
    public Vector2 nodeSize = new Vector2(120f, 60f);
    public Color lineColor = Color.white;
    public float contentPadding = 100f;

    private Dictionary<int, Button> nodeButtons = new Dictionary<int, Button>();
    private List<Image> spawnedLines = new List<Image>();

    public void BuildFromNodes(List<MapNode> nodes)
    {
        ClearMap();

        if (nodes == null || nodes.Count == 0)
            return;

        // compute bounding box of node positions
        float minX = float.PositiveInfinity, minY = float.PositiveInfinity;
        float maxX = float.NegativeInfinity, maxY = float.NegativeInfinity;
        foreach (var n in nodes)
        {
            minX = Mathf.Min(minX, n.pos.x);
            minY = Mathf.Min(minY, n.pos.y);
            maxX = Mathf.Max(maxX, n.pos.x);
            maxY = Mathf.Max(maxY, n.pos.y);
        }

        // center of the node layout in generator coordinates
        Vector2 nodesCenter = new Vector2((minX + maxX) * 0.5f, (minY + maxY) * 0.5f);

        // desired content size to contain all nodes + padding, but at least viewport size
        float contentWidth = Mathf.Max(viewport.rect.width, (maxX - minX) + contentPadding * 2f);
        float contentHeight = Mathf.Max(viewport.rect.height, (maxY - minY) + contentPadding * 2f);

        // apply sizing and ensure the content pivot is centered so anchored positions are relative to center
        mapContainer.pivot = new Vector2(0.5f, 0.5f);
        mapContainer.anchorMin = new Vector2(0.5f, 0.5f);
        mapContainer.anchorMax = new Vector2(0.5f, 0.5f);
        mapContainer.anchoredPosition = Vector2.zero;
        mapContainer.sizeDelta = new Vector2(contentWidth, contentHeight);

        // instantiate node buttons with positions adjusted so the layout is centered in the content
        foreach (var node in nodes)
        {
            var b = Instantiate(nodeButtonPrefab, mapContainer);
            var rt = b.GetComponent<RectTransform>();
            rt.anchoredPosition = node.pos - nodesCenter; // reposition relative to content center
            rt.sizeDelta = nodeSize;

            var text = b.GetComponentInChildren<Text>();
            if (text != null)
                text.text = node.type.ToString();

            // closure safety
            MapNode local = node;
            b.onClick.AddListener(() => OnNodeClicked(local));

            nodeButtons[node.id] = b;
        }

        // instantiate lines (edges) using the already-created button transforms
        foreach (var node in nodes)
        {
            foreach (int targetId in node.connections)
            {
                if (!nodeButtons.ContainsKey(node.id) || !nodeButtons.ContainsKey(targetId))
                    continue;

                var a = nodeButtons[node.id].GetComponent<RectTransform>();
                var b = nodeButtons[targetId].GetComponent<RectTransform>();
                var line = CreateLine(a.anchoredPosition, b.anchoredPosition);
                spawnedLines.Add(line);
            }
        }
    }

    private Image CreateLine(Vector2 a, Vector2 b)
    {
        var go = Instantiate(lineImagePrefab, mapContainer);
        var rt = go.GetComponent<RectTransform>();

        Vector2 diff = b - a;
        float length = diff.magnitude;
        rt.sizeDelta = new Vector2(length, 4f); // thickness
        rt.anchoredPosition = a + diff * 0.5f;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        rt.localRotation = Quaternion.Euler(0f, 0f, angle);
        go.color = lineColor;
        return go;
    }

    private void OnNodeClicked(MapNode node)
    {
        Debug.Log($"MapUI: clicked node {node.id} type {node.type}");

        // store chosen encounter and load battle if it exists
        if (node.encounter != null)
        {
            if (GameSession.Instance != null)
            {
                GameSession.Instance.SelectedEncounter = node.encounter;
            }

            // find a LevelLoader in scene and ask it to load the battle scene
            var loader = FindFirstObjectByType<LevelLoader>();
            if (loader != null)
            {
                loader.LoadBattleScene();
            }
            else
            {
                Debug.LogWarning("MapUI: No LevelLoader found in scene. Add LevelLoader or call your scene transition manually.");
            }
        }
        else
        {
            // handle shops/rest/events etc.
            Debug.Log($"Node {node.id} has no encounter. Handle {node.type} interaction here.");
        }
    }

    private void ClearMap()
    {
        foreach (var kv in nodeButtons)
            if (kv.Value != null) Destroy(kv.Value.gameObject);
        nodeButtons.Clear();

        foreach (var l in spawnedLines)
            if (l != null) Destroy(l.gameObject);
        spawnedLines.Clear();
    }
}
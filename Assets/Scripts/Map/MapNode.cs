using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapNode
{
    public enum NodeType
    {
        Battle,
        Elite,
        Shop,
        Rest,
        Event,
        Treasure,
        Boss
    }

    public int id;
    public Vector2 pos; // UI anchored position in map coordinates
    public NodeType type;
    public EncounterData encounter; // optional, used for battle nodes
    public List<int> connections = new List<int>(); // ids of nodes this node links to
}
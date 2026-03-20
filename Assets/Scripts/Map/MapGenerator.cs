using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Map layout")]
    public int rows = 4;
    public int minNodesPerRow = 1;
    public int maxNodesPerRow = 3;
    public float rowSpacing = 220f;
    public float columnSpacing = 220f;
    public int seed = 0;

    [Header("Encounter pool")]
    public List<EncounterData> battleEncounters; // assign in inspector
    public EncounterData bossEncounter;

    public List<MapNode> GenerateMap()
    {
        var nodes = new List<MapNode>();
        var rng = seed == 0 ? new System.Random() : new System.Random(seed);

        int idCounter = 0;
        var prevRowIds = new List<int>();

        for (int r = 0; r < rows; r++)
        {
            int count = rng.Next(minNodesPerRow, maxNodesPerRow + 1);
            var thisRowIds = new List<int>();

            float rowY = -r * rowSpacing;
            float startX = -((count - 1) * columnSpacing) / 2f;

            for (int i = 0; i < count; i++)
            {
                var node = new MapNode();
                node.id = idCounter++;
                node.pos = new Vector2(startX + i * columnSpacing, rowY);

                // simple node type assignment: last row becomes a boss
                node.type = (r == rows - 1) ? MapNode.NodeType.Boss : MapNode.NodeType.Battle;

                // assign encounters for battle nodes
                if (node.type == MapNode.NodeType.Battle && battleEncounters != null && battleEncounters.Count > 0)
                {
                    node.encounter = battleEncounters[rng.Next(battleEncounters.Count)];
                }
                else if (node.type == MapNode.NodeType.Boss && bossEncounter != null)
                {
                    node.encounter = bossEncounter;
                }

                nodes.Add(node);
                thisRowIds.Add(node.id);
            }

            // create connections from prevRowIds to thisRowIds
            if (prevRowIds.Count > 0)
            {
                foreach (int pid in prevRowIds)
                {
                    // connect each node in previous row to 1..thisRow.Count node(s)
                    int connections = rng.Next(1, thisRowIds.Count + 1);
                    for (int c = 0; c < connections; c++)
                    {
                        int target = thisRowIds[rng.Next(thisRowIds.Count)];
                        if (!nodes[pid].connections.Contains(target))
                            nodes[pid].connections.Add(target);
                    }
                }
            }

            prevRowIds = thisRowIds;
        }

        return nodes;
    }
}
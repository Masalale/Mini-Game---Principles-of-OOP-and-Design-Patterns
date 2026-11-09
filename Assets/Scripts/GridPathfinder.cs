using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class GridPathfinder
{
    private static readonly Vector3Int[] Directions =
    {
        Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right
    };

    // Algorithm: breadth-first search (BFS) over the maze grid.
    // Every step costs the same, so the first time BFS reaches the goal is the shortest path.
    // Worst case visits every floor tile once: O(W x H).
    public static Vector3Int GetNextStep(Tilemap walls, Vector3Int start, Vector3Int goal)
    {
        if (start == goal)
        {
            return start;
        }

        BoundsInt bounds = walls.cellBounds;
        Queue<Vector3Int> frontier = new Queue<Vector3Int>();
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();

        frontier.Enqueue(start);
        cameFrom[start] = start;

        while (frontier.Count > 0)
        {
            Vector3Int current = frontier.Dequeue();
            if (current == goal)
            {
                break;
            }

            foreach (Vector3Int direction in Directions)
            {
                Vector3Int next = current + direction;
                if (!bounds.Contains(next) || walls.HasTile(next) || cameFrom.ContainsKey(next))
                {
                    continue;
                }
                cameFrom[next] = current;
                frontier.Enqueue(next);
            }
        }

        if (!cameFrom.ContainsKey(goal))
        {
            return start;
        }

        // Walk back from the goal until we find the tile right next to the start
        Vector3Int step = goal;
        while (cameFrom[step] != start)
        {
            step = cameFrom[step];
        }
        return step;
    }
}

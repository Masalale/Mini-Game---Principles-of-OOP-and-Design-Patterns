using System.Collections.Generic;
using UnityEngine;

public class WandererEnemy : Enemy
{
    private static readonly Vector3Int[] Directions =
    {
        Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right
    };

    private Vector3Int previousCell;
    private bool hasPrevious;

    protected override Vector3Int GetNextTile(Vector3Int current)
    {
        List<Vector3Int> options = new List<Vector3Int>();
        foreach (Vector3Int direction in Directions)
        {
            Vector3Int next = current + direction;
            // Don't turn straight back unless it's a dead end, otherwise it jitters in place
            if (IsWalkable(next) && !(hasPrevious && next == previousCell))
            {
                options.Add(next);
            }
        }

        Vector3Int chosen = current;
        if (options.Count > 0)
        {
            chosen = options[Random.Range(0, options.Count)];
        }
        else if (hasPrevious)
        {
            chosen = previousCell;
        }

        previousCell = current;
        hasPrevious = true;
        return chosen;
    }
}

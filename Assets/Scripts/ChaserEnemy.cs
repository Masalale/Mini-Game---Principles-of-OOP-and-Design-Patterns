using UnityEngine;

public class ChaserEnemy : Enemy
{
    protected override Vector3Int GetNextTile(Vector3Int current)
    {
        Vector3Int playerCell = walls.WorldToCell(player.position);
        return GridPathfinder.GetNextStep(walls, current, playerCell);
    }
}

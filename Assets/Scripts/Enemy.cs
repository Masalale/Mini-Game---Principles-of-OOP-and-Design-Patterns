using UnityEngine;
using UnityEngine.Tilemaps;

// Inheritance: ChaserEnemy and WandererEnemy reuse this tile-by-tile movement
// and only decide which tile to go to next.
public abstract class Enemy : MonoBehaviour, ITouchable
{
    [SerializeField] private float speed = 2f;

    protected Tilemap walls;
    protected Transform player;

    private Vector3Int currentCell;
    private Vector3 targetPosition;

    public void Init(Tilemap wallsMap, Transform playerTransform)
    {
        walls = wallsMap;
        player = playerTransform;

        currentCell = walls.WorldToCell(transform.position);
        targetPosition = walls.GetCellCenterWorld(currentCell);
        transform.position = targetPosition;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Only pick a new tile once we've reached the centre of the current one,
        // so enemies stay lined up with the grid.
        if (transform.position == targetPosition)
        {
            currentCell = GetNextTile(currentCell);
            targetPosition = walls.GetCellCenterWorld(currentCell);
        }
    }

    // Polymorphism: each enemy type overrides how it picks its next tile
    protected abstract Vector3Int GetNextTile(Vector3Int current);

    protected bool IsWalkable(Vector3Int cell)
    {
        return walls.cellBounds.Contains(cell) && !walls.HasTile(cell);
    }

    public void OnPlayerTouched()
    {
        GameManager.Instance.EndGame(false);
    }
}

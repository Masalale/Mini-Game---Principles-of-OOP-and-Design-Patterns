using UnityEngine;
using UnityEngine.Tilemaps;

public enum EnemyType
{
    Chaser,
    Wanderer
}

// Factory: the only place that knows which prefab each enemy type uses and how to set it up
public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private ChaserEnemy chaserPrefab;
    [SerializeField] private WandererEnemy wandererPrefab;
    [SerializeField] private Tilemap walls;
    [SerializeField] private Transform player;
    [SerializeField] private Transform chaserSpawn;
    [SerializeField] private Transform wandererSpawn;
    [SerializeField] private int potionsPerNewEnemy = 3;

    private void Start()
    {
        // Observer: spawn a new ghost every few potions
        GameManager.Instance.PotionCollected += OnPotionCollected;

        Create(EnemyType.Chaser, chaserSpawn.position);
        Create(EnemyType.Wanderer, wandererSpawn.position);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PotionCollected -= OnPotionCollected;
        }
    }

    public Enemy Create(EnemyType type, Vector3 position)
    {
        Enemy prefab;
        switch (type)
        {
            case EnemyType.Chaser:
                prefab = chaserPrefab;
                break;
            default:
                prefab = wandererPrefab;
                break;
        }

        Enemy enemy = Instantiate(prefab, position, Quaternion.identity);
        enemy.Init(walls, player);
        return enemy;
    }

    private void OnPotionCollected(int collected)
    {
        if (collected % potionsPerNewEnemy == 0 && !GameManager.Instance.IsGameOver)
        {
            Create(EnemyType.Wanderer, wandererSpawn.position);
        }
    }
}

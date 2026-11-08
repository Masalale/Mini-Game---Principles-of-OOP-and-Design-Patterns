using System;
using UnityEngine;

// Singleton: one GameManager per scene, reached through GameManager.Instance
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Observer: HUD and EnemyFactory subscribe to these without GameManager knowing about them
    public event Action<int> PotionCollected;
    public event Action<bool> GameOver;

    [SerializeField] private Transform potionsParent;

    // Encapsulation: anyone can read these, only GameManager can change them
    public int PotionsCollected { get; private set; }
    public int TotalPotions { get; private set; }
    public float ElapsedTime { get; private set; }
    public bool IsGameOver { get; private set; }
    public Leaderboard Leaderboard { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        TotalPotions = potionsParent.childCount;
        Leaderboard = new Leaderboard();
    }

    private void Update()
    {
        if (!IsGameOver)
        {
            ElapsedTime += Time.deltaTime;
        }
    }

    public void CollectPotion()
    {
        if (IsGameOver)
        {
            return;
        }

        PotionsCollected++;
        PotionCollected?.Invoke(PotionsCollected);

        if (PotionsCollected == TotalPotions)
        {
            EndGame(true);
        }
    }

    public void EndGame(bool won)
    {
        if (IsGameOver)
        {
            return;
        }

        IsGameOver = true;
        if (won)
        {
            Leaderboard.AddTime(ElapsedTime);
        }
        GameOver?.Invoke(won);
    }
}

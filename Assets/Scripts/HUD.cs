using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text potionsText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text endTitleText;
    [SerializeField] private TMP_Text leaderboardText;

    private void Start()
    {
        // Observer: HUD reacts to GameManager events instead of GameManager updating the UI
        GameManager.Instance.PotionCollected += OnPotionCollected;
        GameManager.Instance.GameOver += OnGameOver;

        OnPotionCollected(GameManager.Instance.PotionsCollected);
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PotionCollected -= OnPotionCollected;
            GameManager.Instance.GameOver -= OnGameOver;
        }
    }

    private void Update()
    {
        timerText.text = "Time: " + GameManager.Instance.ElapsedTime.ToString("0.0") + "s";
    }

    private void OnPotionCollected(int collected)
    {
        potionsText.text = "Potions: " + collected + " / " + GameManager.Instance.TotalPotions;
    }

    private void OnGameOver(bool won)
    {
        endPanel.SetActive(true);
        endTitleText.text = won ? "You escaped!" : "Caught by a ghost!";

        List<float> times = GameManager.Instance.Leaderboard.GetTimes();
        string text = "Best times\n";
        if (times.Count == 0)
        {
            text += "No escapes yet";
        }
        for (int i = 0; i < times.Count; i++)
        {
            text += (i + 1) + ".  " + times[i].ToString("0.0") + "s\n";
        }
        leaderboardText.text = text;
    }

    // Wired to the Restart button's OnClick in the Inspector
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image[] potionIcons;
    [SerializeField] private Color potionNotCollected = new Color(0.25f, 0.22f, 0.3f);
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text endTitleText;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text leaderboardText;
    [SerializeField] private Color winColor = new Color(0.36f, 0.84f, 0.54f);
    [SerializeField] private Color loseColor = new Color(0.88f, 0.27f, 0.25f);

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
        timerText.text = GameManager.Instance.ElapsedTime.ToString("0.0") + "s";
    }

    // One icon per potion in the maze; collected ones light up
    private void OnPotionCollected(int collected)
    {
        for (int i = 0; i < potionIcons.Length; i++)
        {
            potionIcons[i].color = i < collected ? Color.white : potionNotCollected;
        }
    }

    private void OnGameOver(bool won)
    {
        endPanel.SetActive(true);
        endTitleText.text = won ? "You escaped!" : "Caught by a ghost!";
        endTitleText.color = won ? winColor : loseColor;

        if (won)
        {
            resultText.text = "Your time: " + GameManager.Instance.ElapsedTime.ToString("0.0") + "s";
        }
        else
        {
            resultText.text = "Collect all " + GameManager.Instance.TotalPotions + " potions without touching a ghost.";
        }

        List<float> times = GameManager.Instance.Leaderboard.GetTimes();
        string text = times.Count == 0 ? "No escapes yet" : "";
        for (int i = 0; i < times.Count; i++)
        {
            text += (i + 1) + ".   " + times[i].ToString("0.0") + "s\n";
        }
        leaderboardText.text = text;
    }

    // Wired to the Play again button's OnClick in the Inspector
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

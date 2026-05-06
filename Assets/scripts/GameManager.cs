using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverPanel;
    public GameObject restartButton;
    public CoinPopup coinPopup;
    public int coins = 120;
    public TMP_Text coinText;
    public TMP_Text waveText;
    public int currentWave = 1;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateCoinText();
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateCoinText();
            Debug.Log("Coin kaldı: " + coins);
            return true;
        }

        Debug.Log("Yeterli coin yok!");
        return false;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinText();

        if (coinPopup != null)
        {
            coinPopup.ShowCoin(amount);
        }

        Debug.Log("Coin: " + coins);
    }

    public void UpdateCoinText()
    {
        if (coinText != null)
            coinText.text = "Coin: " + coins;

        if (waveText != null)
            waveText.text = "Wave: " + currentWave;
    }
    public void GameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
using UnityEngine;
using TMPro;

public class CastleHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public TMP_Text healthText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateUI();

        if (currentHealth <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

    void UpdateUI()
    {
        if (healthText != null)
            healthText.text = "Castle HP: " + currentHealth;
    }
}
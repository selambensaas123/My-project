using UnityEngine;
using TMPro;

public class CoinPopup : MonoBehaviour
{
    public TMP_Text popupText;
    public float showTime = 1f;

    private int currentAmount = 0;
    private float timer = 0f;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                currentAmount = 0;
                gameObject.SetActive(false);
            }
        }
    }

    public void ShowCoin(int amount)
    {
        currentAmount += amount;

        popupText.text = "+" + currentAmount + " Coin";

        gameObject.SetActive(true);

        timer = showTime;
    }
}
using UnityEngine;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public static UpgradeUI instance;

    public GameObject panel;
    public TMP_Text infoText;

    private TowerUpgrade selectedTower;

    void Awake()
    {
        instance = this;
    }

    public void OpenPanel(TowerUpgrade tower)
    {
        selectedTower = tower;

        panel.SetActive(true);
        infoText.text =
            "Upgrade Cost: " + tower.upgradeCost +
            "\nSell Price: " + tower.sellPrice;
    }

    public void ClosePanel()
    {
        selectedTower = null;
        panel.SetActive(false);
    }

    public void UpgradeSelectedTower()
    {
        if (selectedTower != null)
        {
            selectedTower.UpgradeTower();
            ClosePanel();
        }
    }

    public void SellSelectedTower()
    {
        if (selectedTower != null)
        {
            selectedTower.SellTower();
            ClosePanel();
        }
    }
}
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    public GameObject upgradedTowerPrefab;
    public int upgradeCost = 100;
    public int sellPrice = 30;

    void OnMouseDown()
    {
        UpgradeUI.instance.OpenPanel(this);
    }

    public void UpgradeTower()
    {
        if (upgradedTowerPrefab == null)
        {
            Debug.Log("Upgrade prefab yok!");
            return;
        }

        if (!GameManager.instance.SpendCoins(upgradeCost))
        {
            Debug.Log("Upgrade için para yetmedi!");
            return;
        }

        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        Instantiate(upgradedTowerPrefab, pos, rot);
        Destroy(gameObject);
    }

    public void SellTower()
    {
        GameManager.instance.AddCoins(sellPrice);
        Destroy(gameObject);
    }
}
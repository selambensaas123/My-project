using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public float towerHeight = 1f;
    private bool hasTower = false;

    void OnMouseDown()
    {
        if (hasTower) return;

        if (TowerManager.instance.selectedTowerPrefab == null)
        {
            Debug.Log("Önce kule seç!");
            return;
        }

        if (!GameManager.instance.SpendCoins(TowerManager.instance.selectedTowerCost))
            return;

        Vector3 spawnPos = transform.position;
        spawnPos.y += towerHeight;

        Instantiate(TowerManager.instance.selectedTowerPrefab, spawnPos, Quaternion.identity);

        TowerManager.instance.HidePreview();

        hasTower = true;
    }
}
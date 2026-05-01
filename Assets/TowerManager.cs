using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;

    public GameObject selectedTowerPrefab;

    void Awake()
    {
        instance = this;
    }

    public void SelectTower(GameObject towerPrefab)
    {
        selectedTowerPrefab = towerPrefab;
        Debug.Log("Seçilen kule: " + towerPrefab.name);
    }
}
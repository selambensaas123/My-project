using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public GameObject towerPrefab;
    public float towerHeight = 1f;

    private bool hasTower = false;

    void OnMouseDown()
    {
        if (hasTower) return;

        Vector3 spawnPos = transform.position;
        spawnPos.y += towerHeight;

        Instantiate(towerPrefab, spawnPos, Quaternion.identity);

        hasTower = true;
    }
}
using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    public GameObject towerPrefab;
    public float towerHeight = 0.5f;

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
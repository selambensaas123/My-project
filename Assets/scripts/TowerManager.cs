using UnityEngine;
using TMPro;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;

    public GameObject selectedTowerPrefab;
    public int selectedTowerCost;

    public GameObject cannonTower;
    public GameObject crossbowTower;
    public GameObject ballistaTower;
    public GameObject turretTower;

    public TMP_Text selectedTowerText;

    private GameObject previewTower;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        UpdatePreviewPosition();
    }

    public void SelectCannon()
    {
        SelectTower(cannonTower, 50, "Cannon");
    }

    public void SelectCrossbow()
    {
        SelectTower(crossbowTower, 75, "Crossbow");
    }

    public void SelectBallista()
    {
        SelectTower(ballistaTower, 100, "Ballista");
    }

    public void SelectTurret()
    {
        SelectTower(turretTower, 120, "Turret");
    }

    void SelectTower(GameObject towerPrefab, int cost, string towerName)
    {
        selectedTowerPrefab = towerPrefab;
        selectedTowerCost = cost;

        if (selectedTowerText != null)
            selectedTowerText.text = "Selected: " + towerName + " (" + cost + ")";

        CreatePreview();
    }

    void CreatePreview()
    {
        if (previewTower != null)
            Destroy(previewTower);

        if (selectedTowerPrefab == null) return;

        previewTower = Instantiate(selectedTowerPrefab);
        previewTower.name = "TowerPreview";

        DisablePreviewScripts(previewTower);
        SetPreviewTransparent(previewTower);
    }

    void UpdatePreviewPosition()
    {
        if (previewTower == null) return;

        Ray ray = Camera.main.ScreenPointToRay(UnityEngine.InputSystem.Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            previewTower.transform.position = hit.point + Vector3.up * 0.5f;
        }
    }

    void DisablePreviewScripts(GameObject obj)
    {
        foreach (MonoBehaviour script in obj.GetComponentsInChildren<MonoBehaviour>())
        {
            script.enabled = false;
        }

        foreach (Collider col in obj.GetComponentsInChildren<Collider>())
        {
            col.enabled = false;
        }

        foreach (AudioSource audio in obj.GetComponentsInChildren<AudioSource>())
        {
            audio.enabled = false;
        }
    }

    void SetPreviewTransparent(GameObject obj)
    {
        foreach (Renderer r in obj.GetComponentsInChildren<Renderer>())
        {
            Color c = r.material.color;
            c.a = 0.4f;
            r.material.color = c;
        }
    }

    public void HidePreview()
    {
        if (previewTower != null)
            Destroy(previewTower);
    }
}
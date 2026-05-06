using UnityEngine;

public class TowerHoverInfo : MonoBehaviour
{
    public GameObject rangeIndicator;

    void OnMouseEnter()
    {
        if (rangeIndicator != null)
            rangeIndicator.SetActive(true);
    }

    void OnMouseExit()
    {
        if (rangeIndicator != null)
            rangeIndicator.SetActive(false);
    }
}
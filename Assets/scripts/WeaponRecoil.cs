using UnityEngine;
using System.Collections;

public class WeaponRecoil : MonoBehaviour
{
    public Transform recoilPart;
    public float recoilDistance = 0.2f;
    public float recoilSpeed = 8f;
    public bool moveUpDown = false;

    private Vector3 originalLocalPosition;
    private bool isRecoiling = false;

    void Start()
    {
        if (recoilPart != null)
            originalLocalPosition = recoilPart.localPosition;
    }

    public void PlayRecoil()
    {
        if (!isRecoiling && recoilPart != null)
            StartCoroutine(RecoilRoutine());
    }

    IEnumerator RecoilRoutine()
    {
        isRecoiling = true;

        Vector3 recoilDirection = moveUpDown ? Vector3.up : -Vector3.forward;
        Vector3 recoilPosition = originalLocalPosition + recoilDirection * recoilDistance;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * recoilSpeed;
            recoilPart.localPosition = Vector3.Lerp(originalLocalPosition, recoilPosition, t);
            yield return null;
        }

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * recoilSpeed;
            recoilPart.localPosition = Vector3.Lerp(recoilPosition, originalLocalPosition, t);
            yield return null;
        }

        isRecoiling = false;
    }
}
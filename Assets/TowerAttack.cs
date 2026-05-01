using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public float range = 8f;
    public float fireRate = 1f;
    public float damage = 25f;

    public GameObject projectilePrefab;
    public Transform firePoint;

    private float fireCooldown = 1f;

    void Update()
    {
        if (projectilePrefab == null)
        {
            Debug.Log("Projectile Prefab boş!");
            return;
        }

        fireCooldown -= Time.deltaTime;

        GameObject target = FindNearestEnemy();

        if (target != null)
        {
            Debug.Log("Düşman bulundu: " + target.name);

            if (fireCooldown <= 0f)
            {
                Shoot(target);
                fireCooldown = 1f / fireRate;
            }
        }
        else
        {
            Debug.Log("Düşman bulunamadı");
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject nearest = null;
        float shortestDistance = range;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= shortestDistance)
            {
                shortestDistance = distance;
                nearest = enemy;
            }
        }

        return nearest;
    }

    void Shoot(GameObject enemy)
    {
        Vector3 spawnPosition = firePoint != null
            ? firePoint.position
            : transform.position + Vector3.up * 1f;

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        Projectile projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript == null)
        {
            Destroy(projectile);
            return;
        }

        projectileScript.SetTarget(enemy.transform, damage);
    }
}
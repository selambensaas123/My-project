using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shootSound;

    public float range = 8f;
    public float fireRate = 1f;
    public float damage = 25f;

    public GameObject projectilePrefab;
    public Transform[] firePoints;

    private float fireCooldown = 1f;

    void Update()
    {
        if (projectilePrefab == null) return;
        if (fireRate <= 0f) fireRate = 1f;

        fireCooldown -= Time.deltaTime;

        if (fireCooldown > 0f) return;

        GameObject target = FindNearestEnemy();

        if (target != null)
        {
            LookAtEnemy(target);

            Shoot(target);
            fireCooldown = 1f / fireRate;
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
        WeaponRecoil recoil = GetComponent<WeaponRecoil>();
        if (recoil != null)
        {
            recoil.PlayRecoil();
        }

        if (firePoints != null && firePoints.Length > 0)
        {
            foreach (Transform point in firePoints)
            {
                if (point == null) continue;
                CreateProjectile(point.position, enemy);
            }
        }
        else
        {
            CreateProjectile(transform.position + Vector3.up * 1f, enemy);
        }

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    void CreateProjectile(Vector3 spawnPosition, GameObject enemy)
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        Projectile projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript == null)
        {
            Destroy(projectile);
            return;
        }

        projectileScript.SetTarget(enemy.transform, damage);
    }
    void LookAtEnemy(GameObject enemy)
    {
        Vector3 direction = enemy.transform.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * 8f
            );
        }
    }
}
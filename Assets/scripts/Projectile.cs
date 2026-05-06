using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8f;
    public float damage = 10f;
    public float lifeTime = 5f;

    private Transform target;

    public void SetTarget(Transform newTarget, float newDamage)
    {
        target = newTarget;
        damage = newDamage;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 hitPoint = target.position + Vector3.up * 0.8f;

        transform.position = Vector3.MoveTowards(
            transform.position,
            hitPoint,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, hitPoint) < 0.25f)
        {
            EnemyMove enemy = target.GetComponent<EnemyMove>();

            if (enemy != null)
                enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 2f;
    public float groundOffset = 0.05f;

    public float bobAmount = 0.08f;
    public float bobSpeed = 8f;
    public float swayAmount = 3f;

    public float health = 50f; // 👈 BURAYA TAŞINDI

    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private float walkTimer = 0f;

    public void SetWaypoints(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypointIndex];

        Vector3 targetPosition = target.position;
        targetPosition.y = transform.position.y;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

        WalkEffect();
        SnapToGround();

        if (Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(target.position.x, 0, target.position.z)) < 0.2f)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                Destroy(gameObject);
            }
        }
    }

    // 👇 HASAR SİSTEMİ BURADA (CLASS İÇİNDE)
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void WalkEffect()
    {
        walkTimer += Time.deltaTime * bobSpeed;

        float rotationZ = Mathf.Sin(walkTimer) * swayAmount;
        transform.rotation *= Quaternion.Euler(0, 0, rotationZ * Time.deltaTime);
    }

    void SnapToGround()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 5f, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 20f))
        {
            transform.position = new Vector3(
                transform.position.x,
                hit.point.y + groundOffset,
                transform.position.z
            );
        }
    }
}
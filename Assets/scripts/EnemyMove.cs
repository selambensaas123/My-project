using UnityEngine;
using TMPro;

public class EnemyMove : MonoBehaviour
{
    public float speed = 2f;
    public float groundOffset = 0.05f;

    public float bobAmount = 0.08f;
    public float bobSpeed = 8f;
    public float swayAmount = 3f;

    public float health = 50f;
    public int coinReward = 1;

    public GameObject deathEffect;
    public AudioClip deathSound;
    public GameObject floatingTextPrefab;

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

        float distance = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(target.position.x, 0, target.position.z)
        );

        if (distance < 0.2f)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                CastleHealth castle = FindFirstObjectByType<CastleHealth>();

                if (castle != null)
                {
                    castle.TakeDamage(10);
                }

                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.instance.AddCoins(coinReward);

        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        if (floatingTextPrefab != null)
        {
            GameObject textObj = Instantiate(
                floatingTextPrefab,
                transform.position + Vector3.up * 1.5f,
                Quaternion.identity
            );

            TMP_Text text = textObj.GetComponentInChildren<TMP_Text>();

            if (text != null)
            {
                text.text = "+" + coinReward;
            }
        }

        Destroy(gameObject);
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
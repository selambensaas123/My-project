using UnityEngine;

public class WaveSpawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] bossPrefabs;

    public Transform spawnPoint;
    public Transform[] wayPoints;

    public void SpawnRandomEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0) return;

        GameObject selectedEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        SpawnEnemyObject(selectedEnemy);
    }

    public void SpawnBoss()
    {
        if (bossPrefabs == null || bossPrefabs.Length == 0) return;

        GameObject selectedBoss = bossPrefabs[Random.Range(0, bossPrefabs.Length)];
        SpawnEnemyObject(selectedBoss);
    }

    void SpawnEnemyObject(GameObject prefab)
    {
        if (prefab == null) return;
        if (spawnPoint == null) return;
        if (wayPoints == null || wayPoints.Length == 0) return;

        GameObject enemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();

        if (enemyMove != null)
        {
            enemyMove.SetWaypoints(wayPoints);
        }
    }
}
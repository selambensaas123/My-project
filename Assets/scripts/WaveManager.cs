using UnityEngine;
using TMPro;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public WaveSpawn[] spawners;

    public int baseEnemyCount = 5;
    public float enemyInterval = 1f;

    public TMP_Text waveStartText;
    public GameObject startWaveButton;

    private bool waveRunning = false;

    public void StartNextWave()
    {
        if (waveRunning) return;

        startWaveButton.SetActive(false);
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        waveRunning = true;

        int currentWave = GameManager.instance.currentWave;

        if (waveStartText != null)
        {
            waveStartText.gameObject.SetActive(true);
            waveStartText.text = "WAVE " + currentWave + " START!";
        }

        yield return new WaitForSeconds(2f);

        if (waveStartText != null)
            waveStartText.gameObject.SetActive(false);

        int enemyCount = baseEnemyCount + (currentWave - 1) * 2;
        bool bossWave = currentWave % 5 == 0;

        if (bossWave)
            enemyCount = Mathf.RoundToInt(enemyCount * 0.6f);

        for (int i = 0; i < enemyCount; i++)
        {
            foreach (WaveSpawn spawner in spawners)
            {
                spawner.SpawnRandomEnemy();
            }

            yield return new WaitForSeconds(enemyInterval);
        }

        if (bossWave)
        {
            foreach (WaveSpawn spawner in spawners)
            {
                spawner.SpawnBoss();
            }
        }

        yield return new WaitUntil(() =>
            FindObjectsByType<EnemyMove>(FindObjectsSortMode.None).Length == 0
        );

        GameManager.instance.currentWave++;
        GameManager.instance.UpdateCoinText();

        waveRunning = false;
        startWaveButton.SetActive(true);
    }
}
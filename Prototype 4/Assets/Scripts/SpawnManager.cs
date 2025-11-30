using UnityEngine;
using UnityTimer;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    public GameObject bossPrefab;

    public float offsetRange = 8.0f;
    public float powerupSpawnDelay = 10.0f;
    public float bossWavePowerupSpawnDelay = 3.0f;
    private float PowerupSpawnDelay => IsBossWave ? bossWavePowerupSpawnDelay : powerupSpawnDelay;

    public int bossSpawnInterval = 5;
    private bool IsBossWave => wave % bossSpawnInterval == 0;

    private int wave = 1;

    private void Start()
    {
        Spawner.SpawnRandom(powerupPrefabs, transform.position, offsetRange);
    }

    private void Update()
    {
        if (Enemy.enemyCount == 0)
        {
            SpawnWave(wave++ + 2);
        }
    }

    public void WaitThenSpawnPowerup()
    {
        this.AttachTimer(PowerupSpawnDelay, () =>
        {
            Spawner.SpawnRandom(powerupPrefabs, transform.position, offsetRange);

        });
    }

    private void SpawnWave(int enemyCount)
    {
        if ((wave - 1) % bossSpawnInterval == 0 && wave != 0)
        {
            for (int i = 0; i < wave / bossSpawnInterval; i++)
            {
                Instantiate(bossPrefab, new Vector3(0, 15.0f, 0), Quaternion.identity);
            }
            return;
        }

        for (int i = 0; i < enemyCount; i++)
        {
            Spawner.SpawnRandom(enemyPrefabs, transform.position, offsetRange);
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityTimer;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;

    public float offsetRange = 8.0f;
    public float powerupSpawnDelay = 10.0f;

    private int wave = 1;

    private void Start()
    {
        SpawnRandom(powerupPrefabs);
    }

    private void Update()
    {
        if (Enemy.enemyCount == 0)
        {
            SpawnWave(wave++);
        }
    }

    public void WaitThenSpawnPowerup()
    {
        this.AttachTimer(powerupSpawnDelay, () =>
        {
            SpawnRandom(powerupPrefabs);

        });
    }

    private void SpawnWave(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnRandom(enemyPrefabs);
        }
    }

    private void SpawnRandom(GameObject[] prefabs)
    {
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Spawn(prefab);
    }

    private void Spawn(GameObject prefab)
    {
        Instantiate(prefab, GenerateSpawnPosition(), prefab.transform.rotation);
    }

    private Vector3 GenerateSpawnPosition()
    {
        Vector3 offset = new Vector3(Random.Range(-offsetRange, offsetRange), 0, Random.Range(-offsetRange, offsetRange));
        return transform.position + offset;
    }
}

using System.Collections;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnCooldown = 7.0f;
    public int minSpawnCount = 1;
    public int maxSpawnCount = 3;

    private void Start()
    {
        StartCoroutine(SpawnEnemiesCoroutine());    
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while(true)
        {
            for (int i = 0; i < Random.Range(minSpawnCount, maxSpawnCount); i++)
            {
                Spawner.SpawnRandom(enemyPrefabs, Vector3.zero);
            }

            yield return new WaitForSeconds(spawnCooldown);
        }
    }
}

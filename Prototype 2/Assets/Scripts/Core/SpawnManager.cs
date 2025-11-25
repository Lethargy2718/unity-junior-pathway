using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public Vector2 spawnInterval = new Vector2(0.8f, 2.0f);

    public float spawnRangeX = 12f;
    public float spawnRangeZ = 7f;

    public float spawnPosX = 25f;
    public float spawnPosZ = 15f;
    
    private void Start()
    {
        StartCoroutine(SpawnAnimalCoroutine());
    }

    private IEnumerator SpawnAnimalCoroutine()
    {
        while (true)
        {
            SpawnAnimal();
            yield return new WaitForSeconds(Random.Range(spawnInterval.x, spawnInterval.y));
        }
    }

    private void SpawnAnimal()
    {
        GameObject animal = animalPrefabs[Random.Range(0, animalPrefabs.Length)];

        float prob = Random.Range(0f, 1f);

        Vector3 spawnPos;
        Vector3 direction = Vector3.zero;

        if (prob < 0.25f)
        {
            // Left
            float zPos = Random.Range(-spawnRangeZ, spawnRangeZ);
            spawnPos = new Vector3(-spawnPosX, 0, zPos);
            direction.x = 1;
        }
        else if (prob < 0.5f)
        {
            // Right
            float zPos = Random.Range(-spawnRangeZ, spawnRangeZ);
            spawnPos = new Vector3(spawnPosX, 0, zPos);
            direction.x = -1;
        }
        else if (prob < 0.75f)
        {
            // Top
            float xPos = Random.Range(-spawnRangeX, spawnRangeX);
            spawnPos = new Vector3(xPos, 0, spawnPosZ);
            direction.z = -1;
        }
        else
        {
            // Bottom
            float xPos = Random.Range(-spawnRangeX, spawnRangeX);
            spawnPos = new Vector3(xPos, 0, -spawnPosZ);
            direction.z = 1;
        }

        Instantiate(animal, spawnPos, Quaternion.LookRotation(direction));
    }
}

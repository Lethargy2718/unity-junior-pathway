using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public Vector2 spawnInterval = new Vector2(0.8f, 2.0f);
    public float spawnRangeX = 10.0f;
    public float spawnPosZ = 20.0f;

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

        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPos = new Vector3(xPos, 0, spawnPosZ);

        Instantiate(animal, spawnPos, Quaternion.Euler(new Vector3(0,180,0)));
    }
}

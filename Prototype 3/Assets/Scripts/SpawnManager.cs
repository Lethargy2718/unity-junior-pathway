using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Vector2 spawnInterval = new Vector2(0.5f, 1.0f);

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(Random.Range(spawnInterval.x, spawnInterval.y));
        }
    }

    private void Spawn()
    {
        Instantiate(obstaclePrefab, transform.position, obstaclePrefab.transform.rotation);
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += StopAllCoroutines;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= StopAllCoroutines;
    }
}

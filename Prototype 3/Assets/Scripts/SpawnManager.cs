using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;

    public float minDistance = 5f;
    public float maxDistance = 10f;

    private IEnumerator Start()
    {
        while (true)
        {
            Spawn();

            float speed = GameManager.Instance.Speed;

            float distance = Random.Range(minDistance, maxDistance);
            float delay = distance / speed;

            yield return new WaitForSeconds(delay);
        }
    }

    private void Spawn()
    {
        Instantiate(obstaclePrefab, transform.position, obstaclePrefab.transform.rotation);
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += StopSpawning;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= StopSpawning;
    }

    private void StopSpawning()
    {
        StopAllCoroutines();
        enabled = false;
    }
}

using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private ParticleSystem psPrefab;
    private float spawnTime;
    private GameObject parent;
    private void Awake()
    {
        parent = transform.parent.gameObject;
    }

    private void Start()
    {
        spawnTime = Time.time;
    }

    public void Initialize(float lifetime)
    {
        StartCoroutine(AutoDestroy(lifetime));
    }

    private IEnumerator AutoDestroy(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        GameManager.Instance.missedTargets++;
        Destroy(parent);
    }

    public void GetKilled(Collision collision)
    {
        ParticleSpawner.SpawnAfterCollision(psPrefab, collision);
        GameManager.Instance.successfulShots++;
        Destroy(parent);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        GameManager.Instance.UpdateUI();
        GameManager.Instance.UpdateAvgLifeTime(Time.time - spawnTime);
    }
}

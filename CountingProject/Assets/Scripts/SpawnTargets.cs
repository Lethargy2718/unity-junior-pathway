using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnTargets : MonoBehaviour
{
    private BoxCollider bc;
    public GameObject targetPrefab;
    public float initialDelay = 2.0f;
    public float cooldown = 2.0f;
    public float targetLifeTime = 3.5f;

    private void Awake()
    {
        bc = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        StartCoroutine(SpawnTargetsCoroutine());
    }

    private IEnumerator SpawnTargetsCoroutine()
    {
        yield return new WaitForSeconds(initialDelay);
        while(true)
        {
            SpawnTarget();
            yield return new WaitForSeconds(cooldown);
        }
    }

    private void SpawnTarget()
    {
        Vector3 localPos = new(
            Random.Range(-bc.size.x/ 2, bc.size.x / 2),
            Random.Range(-bc.size.y / 2, bc.size.y / 2),
            0
        );

        Vector3 pos = bc.transform.TransformPoint(localPos);
        Quaternion rotation = Quaternion.LookRotation(transform.forward);
        GameObject target = Instantiate(targetPrefab, pos, rotation);

        Target targetComponent = target.GetComponentInChildren<Target>();
        if (targetComponent != null)
        {
            targetComponent.Initialize(targetLifeTime);
        }
    }
}

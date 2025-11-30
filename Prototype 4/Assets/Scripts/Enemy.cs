using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int enemyCount = 0;
    public float arenaRadius = 40f;
    public Vector3 arenaCenter = new Vector3(0, 1.5f, 0);


    private void Start()
    {
        enemyCount++;
    }

    private void Update()
    {

        if (Vector3.Distance(transform.position, arenaCenter) > arenaRadius || transform.position.y < -3.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        enemyCount--;
    }
}

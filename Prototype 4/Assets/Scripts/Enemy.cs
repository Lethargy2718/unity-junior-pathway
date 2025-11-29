using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int enemyCount = 0;


    private void Start()
    {
        enemyCount++;
    }

    private void Update()
    {
        if (transform.position.y < -5f) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        enemyCount--;
    }
}

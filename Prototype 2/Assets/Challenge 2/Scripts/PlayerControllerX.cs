using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public float cooldown = 1.5f;
    private float lastShootTime = -Mathf.Infinity;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastShootTime + cooldown)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            lastShootTime = Time.time;

        }
    }
}



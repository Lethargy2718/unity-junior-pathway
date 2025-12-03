using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float offsetX = 30f;
    public float offsetZ = 20f;

    void Update()
    {
        if (transform.position.x < -offsetX || transform.position.x > offsetX || transform.position.z < -offsetZ || transform.position.z > offsetZ)
        {
            if (CompareTag("Projectile")) gameObject.SetActive(false);
            else Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 10.0f;
    public float xRange = 10f;
    public GameObject projectile;

    private float input = 0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnProjectile();
        }
    }

    private void FixedUpdate()
    {
        input = Input.GetAxisRaw("Horizontal");
        transform.Translate(input * speed * Time.fixedDeltaTime * Vector3.right);
        Bound();
    }

    private void Bound()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        else if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
    }

    private void SpawnProjectile()
    {
        Instantiate(projectile, transform.position, projectile.transform.rotation);
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10.0f;
    public float rotationSpeed = 3.0f;

    [Header("World Boundaries")]
    public float marginX = 0.1f;
    public float marginY = 0.1f;

    [Header("Projectile")]
    public GameObject projectile;
    public float fireRate = 0.2f;

    private float nextFireTime = 0f;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            SpawnProjectile();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Rotate(direction);
        transform.Translate(speed * Time.fixedDeltaTime * direction, Space.World);
        Bound();
    }

    private void Bound()
    {
        Vector3 viewportPos = cam.WorldToViewportPoint(transform.position);

        Vector3 targetViewportPos;
        targetViewportPos.x = Mathf.Clamp(viewportPos.x, marginX, 1 - marginX);
        targetViewportPos.y = Mathf.Clamp(viewportPos.y, marginY, 1 - marginY);
        targetViewportPos.z = viewportPos.z;

        // Didn't clamp
        if (targetViewportPos == viewportPos) return;

        // Preserve y position as it gets corrupted a little
        float posY = transform.position.y;
        Vector3 newPos = cam.ViewportToWorldPoint(targetViewportPos);
        newPos.y = posY;
        transform.position = newPos;
    }

    private void SpawnProjectile()
    {
        Instantiate(projectile, transform.position, transform.rotation);
    }
    
    private void Rotate(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.Instance.Lives--;
        }
    }
}

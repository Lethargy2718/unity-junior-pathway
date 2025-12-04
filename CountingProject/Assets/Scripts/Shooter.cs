using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float offset = 5f;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;                     
    }

    private void Update()
    {
        LockMouse();

        if (Input.GetMouseButtonDown(0))
        {
            SpawnProjectile(cam.transform.position, cam.transform.forward);
        }
    }

    private void SpawnProjectile(Vector3 startPos, Vector3 direction)
    {
        Vector3 pos = startPos + direction * offset;
        Projectile projectile = Instantiate(projectilePrefab, pos, Quaternion.identity);
        projectile.direction = direction.normalized;
    }


    private void LockMouse()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 30.0f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = speed * transform.forward;
    }
}

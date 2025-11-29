using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float acceleration = 2f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (target == null)
        {
            target = GameObject.Find("Player").transform;
        }
    }

    private void Update()
    {
        Vector3 dir = Vector3.down;
        
        if (target != null)
        {
            dir = (target.position - rb.position).normalized;
        }

        rb.AddForce(acceleration * dir, ForceMode.Acceleration);
    }
}

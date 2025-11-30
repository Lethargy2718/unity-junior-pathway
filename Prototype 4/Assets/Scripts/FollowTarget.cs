using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;

    public enum FollowMode { Acceleration, Velocity }
    public FollowMode mode = FollowMode.Acceleration;

    public float acceleration = 30f;
    public float speed = 30.0f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (target == null)
        {
            target = GameObject.Find("Player").transform;
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 dir = (target.position - transform.position).normalized;

        switch (mode)
        {
            case FollowMode.Acceleration:
                rb.AddForce(dir * acceleration, ForceMode.Acceleration);
                break;

            case FollowMode.Velocity:
                rb.linearVelocity = dir * speed;
                break;
            default:
                break;
        }
    }
}

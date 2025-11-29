using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float knockbackImpulse = 30f;
    public float rotationSpeed = 360;
    private FollowTarget followTarget;

    private void Awake()
    {
       followTarget = GetComponent<FollowTarget>();
    }

    private void Update()
    {
        if (followTarget != null && followTarget.target != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(followTarget.target.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.CompareTag("Enemy") &&
            obj.TryGetComponent<Knockback>(out var enemyKnockback))
        {
            enemyKnockback.ApplyKnockback(transform.position, knockbackImpulse);
        }

        Destroy(gameObject);
    }
}

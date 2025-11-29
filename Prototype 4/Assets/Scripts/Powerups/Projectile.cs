using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float knockbackImpulse = 30f;

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

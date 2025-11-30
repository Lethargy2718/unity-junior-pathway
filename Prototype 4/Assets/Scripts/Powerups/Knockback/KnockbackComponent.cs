using UnityEngine;

public class KnockbackComponent : MonoBehaviour
{
    public float knockbackImpulse = 60.0f;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.CompareTag("Enemy"))
        {
            KnockbackEnemy(obj);
        }
    }

    private void KnockbackEnemy(GameObject enemy)
    {
        if (enemy.TryGetComponent<Knockback>(out var enemyKnockback))
        {
            enemyKnockback.ApplyKnockback(transform.position, knockbackImpulse);
        }
    }
}

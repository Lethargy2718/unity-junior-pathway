using UnityEngine;

public class KnockingEnemy : Enemy
{
    public float knockbackImpulse = 10.0f;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player"))
        {
            if (obj.TryGetComponent<Knockback>(out var playerKnockback))
            {
                playerKnockback.ApplyKnockback(transform.position, knockbackImpulse);
            }
        }
    }
}

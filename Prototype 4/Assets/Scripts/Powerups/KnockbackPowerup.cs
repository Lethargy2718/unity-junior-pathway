using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Knockback")]

public class KnockbackPowerup : Powerup
{
    public float duration = 5.0f;
    public override float Duration => duration;
    public float impulse = 60.0f;

    private KnockbackComponent knockbackComponent;

    public override void OnApply(GameObject target)
    {
        if (target.TryGetComponent<KnockbackComponent>(out var component))
        {
            Destroy(component);
        }

        knockbackComponent = target.AddComponent<KnockbackComponent>();
        knockbackComponent.knockbackImpulse = impulse;
    }

    public override void OnRemove(GameObject target)
    {
        if (knockbackComponent != null)
        {
            Destroy(knockbackComponent);
        }
    }
}

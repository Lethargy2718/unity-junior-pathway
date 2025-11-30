using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Knockback")]

public class KnockbackPowerup : Powerup<KnockbackComponent>
{
    public float duration = 5.0f;
    public override float Duration => duration;
    public float impulse = 60.0f;

    protected override void OnApply(GameObject target)
    {
        component.knockbackImpulse = impulse;
    }
}

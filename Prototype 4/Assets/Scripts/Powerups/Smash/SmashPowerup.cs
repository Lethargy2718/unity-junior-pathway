using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Smash")]

public class SmashPowerup : Powerup<SmashComponent>
{
    public float duration = 5.0f;
    public override float Duration => duration;
    public float ascentTime = 0.23f;
    public float knockbackImpulse = 75f;
    public float maxDistance = 30f;
    public float cooldown = 1f;
    public float verticalSpeed = 60f;

    protected override void OnApply(GameObject target)
    {
        component.ascentTime = ascentTime;
        component.knockbackImpulse = knockbackImpulse;
        component.maxDistance = maxDistance;
        component.cooldown = cooldown;
        component.verticalSpeed = verticalSpeed;
    }
}

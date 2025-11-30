using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Smash")]

public class SmashPowerup : Powerup
{
    public float duration = 5.0f;
    public override float Duration => duration;
    public float ascentTime = 0.23f;
    public float knockbackImpulse = 75f;
    public float maxDistance = 30f;
    public float cooldown = 1f;
    public float verticalSpeed = 60f;


    private SmashComponent smashComponent;

    public override void OnApply(GameObject target)
    {
        if (target.TryGetComponent<SmashComponent>(out var component))
        {
            Destroy(component);
        }

        smashComponent = target.AddComponent<SmashComponent>();
        smashComponent.ascentTime = ascentTime;
        smashComponent.knockbackImpulse = knockbackImpulse;
        smashComponent.maxDistance = maxDistance;
        smashComponent.cooldown = cooldown;
        smashComponent.verticalSpeed = verticalSpeed;
    }

    public override void OnRemove(GameObject target)
    {
        if (smashComponent != null)
        {
            Destroy(smashComponent);
        }
    }
}
